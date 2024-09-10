using MySql.Data.MySqlClient;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;
using System.Globalization;
using System.Xml.Linq;
using System.Data;
using System.Diagnostics;
using K4os.Compression.LZ4.Internal;

namespace transcript.Models
{
    public class regsem
    {
        public int year { get; set; }
        public int sem { get; set; }
    }

    public class stu
    {
        public int stuno { get; set; }
        public int deptno { get; set; }
        public string? name { get; set; }
        public int entryYear { get; set; }
        public string? gradDate { get; set; }
    }

    public class crs
    {
        public int cono { get; set; }
        public string? chinese_co { get; set; }
    }

    public class stu_crs
    {
        public int stuno { get; set; }
        public int year { get; set; }
        public int semester { get; set; }
        public string? stu_course_no { get; set; }
        public decimal stu_course_score { get; set; }
        public string? chinese_co { get; set; }
    }


    public class Student
    {
        public string Name { get; set; }
        public string Enrolled { get; set; }
        public string DegreeConferred { get; set; }
        public string DateConferred { get; set; }
        public string Department { get; set; }
        public string College { get; set; }
        public string Issued { get; set; }
        public decimal Score { get; set; }
        public decimal Credits { get; set; }
        public decimal GPA { get; set; }
    }

    public class Courses
    {
        public string Course { get; set; }
        public decimal Credit { get; set; }
        public string Grade { get; set; }
        public int DataYear { get; set; }
        public int DataSemester { get; set; }
        public decimal TotalCredit { get; set; }
        public decimal Average { get; set; }
    }

    public class Sign
    {
        public byte[] SignReg { get; set; }
        public byte[] SignDean { get; set; }
        public Sign(string path)
        {
            SignReg = File.ReadAllBytes(Path.Combine(path, "signreg.png"));
            SignDean = File.ReadAllBytes(Path.Combine(path, "signdean.png"));
        }
    }

    public class DataBase
    {
        public DataTable showStu(DataTable dt, string connectionString)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string cmd = $"SELECT * FROM stufile";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd, conn);
            adapter.Fill(dt);
            conn.Close();
            return dt;
        }

        public DataTable showCrs(DataTable dt, string connectionString)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            string cmd = $"SELECT * FROM course";
            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd, conn);
            adapter.Fill(dt);
            conn.Close();
            return dt;
        }

        public void setStu(stu Student, string connectionString)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            using var cmd = new MySqlCommand("INSERT INTO stufile(stuno, deptno, name, entryYear, gradDate) VALUES (@stuno, @deptno, @name, @entryYear, @gradDate)");
            cmd.Parameters.AddWithValue("@stuno", Student.stuno);
            cmd.Parameters.AddWithValue("@deptno", Student.deptno);
            cmd.Parameters.AddWithValue("@name", Student.name);
            cmd.Parameters.AddWithValue("@entryYear", Student.entryYear);
            cmd.Parameters.AddWithValue("@gradDate", Student.gradDate);
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void setCrs(stu_crs Crs, string connectionString)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.Open();
            using var cmd = new MySqlCommand("INSERT INTO stu_course(stuno, year, semester, stu_course_no, stu_course_score) VALUES (@stuno, @year, @semester, @stu_course_no, @stu_course_score)");
            cmd.Parameters.AddWithValue("@stuno", Crs.stuno);
            cmd.Parameters.AddWithValue("@year", Crs.year);
            cmd.Parameters.AddWithValue("@semester", Crs.semester);
            cmd.Parameters.AddWithValue("@stu_course_no", Crs.stu_course_no);
            cmd.Parameters.AddWithValue("@stu_course_score", Crs.stu_course_score);
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public List<stu_crs> GetStuCrs(int StudentId, string connectionString)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            List<stu_crs> crs = new List<stu_crs>();
            conn.ConnectionString = connectionString;
            conn.Open();
            using var cmd = new MySqlCommand($"SELECT year, semester, stu_course_no, stu_course_score, chinese_co FROM stu_course LEFT JOIN course ON stu_course.stu_course_no = course.cono WHERE stuno = {StudentId}", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                crs.Add(new stu_crs()
                {
                    stuno = StudentId,
                    year = reader.GetInt32(0),
                    semester = reader.GetInt32(1),
                    stu_course_no = reader.GetString(2),
                    stu_course_score = reader.GetInt32(3), 
                    chinese_co = reader.GetString(4),
                });
            }
            conn.Close();
            return crs;
        }

        public void DelStuCrs(stu_crs crs, string connectionString)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            conn.ConnectionString = connectionString;
            conn.Open();
            using var cmd = new MySqlCommand($"DELETE FROM stu_course WHERE stu_course.stuno = {crs.stuno} AND stu_course.year = {crs.year} AND stu_course.semester = {crs.semester} AND stu_course.stu_course_no = {crs.stu_course_no}", conn);
            cmd.Connection = conn;
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void SemCal(string StudentId, int year, int sem, string connectionString)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            decimal score_sum = 0;
            decimal obtain_credit = 0;
            decimal acc_credit = 0;
            bool exist = false;
            conn.ConnectionString = connectionString;
            conn.Open();
            using (var cmd = new MySqlCommand($"SELECT stu_course_score, credit FROM stu_course LEFT JOIN course ON stu_course.stu_course_no = course.cono WHERE stuno = {StudentId} AND year = {year} AND semester = {sem}", conn))
            {
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    decimal score = reader.GetDecimal(0);
                    decimal credit = reader.GetDecimal(1);
                    score_sum += score * credit;
                    obtain_credit += credit;
                    if (score >= 60)
                        acc_credit += credit;
                }
            }
            using (var cmd = new MySqlCommand($"SELECT * FROM semester WHERE stuno = {StudentId} AND year = {year} AND semester = {sem}", conn))
            {
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                    exist = true;
                
            }
            if (!exist)
            {
                using (var cmd = new MySqlCommand($"INSERT INTO semester(stuno, year, semester, score_semester_sum, score_semester_avg, accumulate_credit, obtain_credit) VALUES (@stuno, @year, @semester, @score_semester_sum, @score_semester_avg, @accumulate_credit, @obtain_credit)", conn))
                {
                    cmd.Parameters.AddWithValue("@stuno", StudentId);
                    cmd.Parameters.AddWithValue("@year", year);
                    cmd.Parameters.AddWithValue("@semester", sem);
                    cmd.Parameters.AddWithValue("@score_semester_sum", score_sum);
                    cmd.Parameters.AddWithValue("@score_semester_avg", score_sum / obtain_credit);
                    cmd.Parameters.AddWithValue("@accumulate_credit", acc_credit);
                    cmd.Parameters.AddWithValue("@obtain_credit", obtain_credit);
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                using (var cmd = new MySqlCommand($"UPDATE semester SET score_semester_sum = {score_sum}, score_semester_avg = {score_sum / obtain_credit}, accumulate_credit = {acc_credit}, obtain_credit = {obtain_credit} WHERE stuno = {StudentId} AND year = {year} AND semester = {sem}", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            conn.Close();
        }

        public void AccCal(string StudentId, string connectionString)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            decimal score_sum = 0;
            decimal obtain_credit = 0;
            decimal acc_credit = 0;
            decimal GPA = 0;
            bool exist = false;
            conn.ConnectionString = connectionString;
            conn.Open();
            using (var cmd = new MySqlCommand($"SELECT stu_course_score, credit FROM stu_course LEFT JOIN course ON stu_course.stu_course_no = course.cono WHERE stuno = {StudentId}", conn))
            {
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    decimal score = reader.GetDecimal(0);
                    decimal credit = reader.GetDecimal(1);
                    score_sum += score * credit;
                    obtain_credit += credit;
                    if (score >= 60)
                        acc_credit += credit;

                    if (score >= 80)
                        GPA += 4 * credit;

                    else if (score >= 70)
                        GPA += 3 * credit;

                    else if (score >= 60)
                        GPA += 2 * credit;

                    else if (score >= 50)
                        GPA += 1 * credit;
                }
            }
            Debug.WriteLine(StudentId);
            using (var cmd = new MySqlCommand($"SELECT * FROM accumulate_info WHERE stuno = {StudentId}", conn))
            {
                using var reader = cmd.ExecuteReader();
                if (reader.Read())
                    exist = true;

            }
            if (!exist)
            {
                using (var cmd = new MySqlCommand($"INSERT INTO accumulate_info(stuno, score_sum, accumulate_credit, obtain_credit, score_avg, gpa_avg) VALUES (@stuno, @score_sum, @accumulate_credit, @obtain_credit, @score_avg, @gpa_avg)", conn))
                {
                    cmd.Parameters.AddWithValue("@stuno", StudentId);
                    cmd.Parameters.AddWithValue("@score_sum", score_sum);
                    cmd.Parameters.AddWithValue("@accumulate_credit", acc_credit);
                    cmd.Parameters.AddWithValue("@obtain_credit", obtain_credit);
                    cmd.Parameters.AddWithValue("@score_avg", score_sum / obtain_credit);
                    cmd.Parameters.AddWithValue("@gpa_avg", GPA / obtain_credit);
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                using (var cmd = new MySqlCommand($"UPDATE accumulate_info SET score_sum = {score_sum}, accumulate_credit = {acc_credit}, obtain_credit = {obtain_credit}, score_avg = {score_sum / obtain_credit}, gpa_avg = {GPA / obtain_credit} WHERE stuno = {StudentId}", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            conn.Close();
        }

        public List<Courses> getCourse(string StudentId, string connectionString)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            List<Courses> crs = new List<Courses>();
            List<regsem> reg = new List<regsem>();
            conn.ConnectionString = connectionString;
            conn.Open();
            using (var cmd = new MySqlCommand($"SELECT DISTINCT year, semester FROM stu_course WHERE stuno = {StudentId}", conn))
            {
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    reg.Add(new regsem()
                    {
                        year = reader.GetInt32(0),
                        sem = reader.GetInt32(1),
                    });
                }
            }
            foreach(var item in reg)
            {
                SemCal(StudentId, item.year, item.sem, connectionString);
            }
            AccCal(StudentId, connectionString);
            using (var cmd = new MySqlCommand($"SELECT chinese_co, credit, stu_course_score, stu_course.year, stu_course.semester, accumulate_credit, score_semester_avg FROM stu_course LEFT JOIN course ON stu_course.stu_course_no = course.cono LEFT JOIN semester ON stu_course.year = semester.year AND stu_course.semester = semester.semester AND stu_course.stuno = semester.stuno WHERE stu_course.stuno = '{StudentId}' AND ((stu_course.year < {reg.Last().year}) OR (stu_course.year = {reg.Last().year} AND stu_course.semester <= {reg.Last().sem})) ORDER BY year, semester", conn))
            {
                using var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    crs.Add(new Courses()
                    {
                        Course = reader.GetString(0),
                        Credit = reader.GetDecimal(1),
                        Grade = reader.GetDecimal(2) < 60 ? "*" + reader.GetDecimal(2).ToString() : reader.GetDecimal(2).ToString(),
                        DataYear = reader.GetInt32(3),
                        DataSemester = reader.GetInt32(4),
                        TotalCredit = reader.GetDecimal(5),
                        Average = reader.GetDecimal(6),
                    });
                }
            }
            conn.Close();
            return crs;
        }

            public List<Student> getStudent(string StudentId, string connectionString)
        {
            MySqlConnection conn = new MySqlConnection(connectionString);
            List<Student> stu = new List<Student>();
            conn.ConnectionString = connectionString;
            conn.Open();
            using var cmd = new MySqlCommand($"SELECT name, entryYear, degreeName, gradDate, deptName, collegeName, score_avg, accumulate_credit, gpa_avg FROM stufile LEFT JOIN pubdep ON stufile.deptno = pubdep.deptno LEFT JOIN accumulate_info ON stufile.stuno = accumulate_info.stuno WHERE stufile.stuno = {StudentId}", conn);
            using var reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                stu.Add(new Student()
                {
                    Name = reader.GetString(0),
                    Enrolled = (reader.GetInt32(1) + 1911).ToString(),
                    DegreeConferred = reader.GetString(2),
                    DateConferred = reader.IsDBNull(3) ? "blank" : (reader.GetInt32(3) + 1911).ToString(),
                    Department = reader.GetString(4),
                    College = reader.GetString(5),
                    Issued = DateTime.Now.ToString("MMMM dd, yyyy", new CultureInfo("en-us")),
                    Score = reader.GetDecimal(6),
                    Credits = reader.GetDecimal(7),
                    GPA = reader.GetDecimal(8)
                });
            }
            conn.Close();
            return stu;
        }
    }
}
