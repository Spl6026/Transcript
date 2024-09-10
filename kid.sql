-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- 主機： 127.0.0.1
-- 產生時間： 2024-01-03 22:06:38
-- 伺服器版本： 10.4.28-MariaDB
-- PHP 版本： 8.2.4

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- 資料庫： `kid`
--

-- --------------------------------------------------------

--
-- 資料表結構 `accumulate_info`
--

CREATE TABLE `accumulate_info` (
  `stuno` int(7) NOT NULL,
  `score_sum` decimal(10,2) NOT NULL,
  `accumulate_credit` decimal(5,2) NOT NULL,
  `obtain_credit` decimal(5,2) NOT NULL,
  `score_avg` decimal(5,2) NOT NULL,
  `gpa_avg` decimal(5,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- 傾印資料表的資料 `accumulate_info`
--

INSERT INTO `accumulate_info` (`stuno`, `score_sum`, `accumulate_credit`, `obtain_credit`, `score_avg`, `gpa_avg`) VALUES
(1102928, 4802.00, 57.00, 60.00, 80.03, 3.48);

-- --------------------------------------------------------

--
-- 資料表結構 `course`
--

CREATE TABLE `course` (
  `cono` varchar(32) NOT NULL,
  `chinese_co` varchar(32) NOT NULL,
  `credit` int(2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- 傾印資料表的資料 `course`
--

INSERT INTO `course` (`cono`, `chinese_co`, `credit`) VALUES
('34700010', '程式設計', 3),
('34700015', '微積分（II）', 3),
('34700016', '資料結構', 3),
('34700017', '物件導向程式設計', 3),
('34700022', '線性代數', 3),
('34700023', '電子電路學', 2),
('34700024', '電子電路學實習', 1),
('34700025', '離散數學', 3),
('34700027', '軟體工程導論', 3),
('34700029', '視窗程式設計', 3),
('34700030', '生物資訊導論', 3),
('34700031', '微積分（I）', 3),
('34700032', '計算機概論', 3),
('34700036', '演算法導論', 3),
('34700037', '程式語言學', 3),
('34700038', '數位系統', 2),
('34700039', '數位系統實習', 1),
('34700041', '計算機圖學', 3),
('34700042', '資料壓縮', 3),
('34700045', '作業系統', 3),
('34700046', '計算機組織', 3),
('34700047', '組合語言與實習', 3),
('34700048', '計算機專題（I）', 1),
('34700049', '計算機專題（II）', 1),
('34700053', '系統程式', 3),
('34700054', '多媒體系統導論', 3),
('34700055', '計算機網路', 3),
('34700058', '資料庫導論', 3),
('34700061', '數值方法', 3),
('34700062', '資訊安全與管理', 3),
('34700065', '網路程式設計', 3),
('34700066', '網際網路服務', 3),
('34700067', '電子商務系統', 3),
('34700070', '資料庫系統設計', 3),
('34700071', '計算機結構', 3),
('34700076', '人工智慧導論', 3),
('34700079', '圖形理論導論', 3),
('34700080', '無線及寬頻網路', 3),
('34700081', '編譯器設計', 3),
('34700084', '自動機與形式語言', 3),
('34700085', '資料探勘導論', 3),
('34700100', '嵌入式系統導論', 3),
('34700107', '機率學', 3),
('34700113', 'Linux 系統管理', 3),
('34700120', '多媒體訊號處理', 3),
('34700123', '多媒體安全理論與實作', 3),
('34700127', '人機互動設計', 3),
('34700129', '遊戲程式設計', 3),
('34700132', '演化計算導論', 3),
('34700134', '行動裝置應用程式設計', 3),
('34700136', '高等程式設計', 3),
('34700138', '物聯網概論', 3),
('34700141', '軟體工程實務', 3),
('34700142', '雲端技術實務', 3),
('34700143', '圖訊辨識導論與剖析', 3),
('34700146', '影像處理導論', 3),
('34700148', '巨量資料分析導論', 3),
('34700149', '區塊鏈技術理論與實作', 3),
('34700151', '安全程式設計與駭客攻防技術', 3),
('34700152', '機器學習導論', 3),
('34700153', '網頁程式設計', 3),
('34700154', '量子計算導論與程式設計', 3),
('34700155', '電腦視覺導論', 3),
('34700157', '軟體定義網路導論', 3);

-- --------------------------------------------------------

--
-- 資料表結構 `pubdep`
--

CREATE TABLE `pubdep` (
  `deptno` varchar(3) NOT NULL,
  `deptName` varchar(32) NOT NULL,
  `degreeName` varchar(32) NOT NULL,
  `collegeName` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- 傾印資料表的資料 `pubdep`
--

INSERT INTO `pubdep` (`deptno`, `deptName`, `degreeName`, `collegeName`) VALUES
('347', '資訊工程學系', '工學學士', '理工學院');

-- --------------------------------------------------------

--
-- 資料表結構 `semester`
--

CREATE TABLE `semester` (
  `stuno` int(7) NOT NULL,
  `year` int(3) NOT NULL,
  `semester` int(1) NOT NULL,
  `score_semester_sum` decimal(10,2) NOT NULL,
  `score_semester_avg` decimal(5,2) NOT NULL,
  `accumulate_credit` decimal(5,2) NOT NULL,
  `obtain_credit` decimal(5,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- 傾印資料表的資料 `semester`
--

INSERT INTO `semester` (`stuno`, `year`, `semester`, `score_semester_sum`, `score_semester_avg`, `accumulate_credit`, `obtain_credit`) VALUES
(1102928, 110, 1, 828.00, 69.00, 9.00, 12.00),
(1102928, 110, 2, 1216.00, 81.07, 15.00, 15.00),
(1102928, 111, 1, 1210.00, 80.67, 15.00, 15.00),
(1102928, 111, 2, 1548.00, 86.00, 18.00, 18.00);

-- --------------------------------------------------------

--
-- 資料表結構 `stufile`
--

CREATE TABLE `stufile` (
  `stuno` int(7) NOT NULL,
  `deptno` int(3) NOT NULL,
  `name` varchar(32) NOT NULL,
  `entryYear` int(3) NOT NULL,
  `gradDate` varchar(5) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- 傾印資料表的資料 `stufile`
--

INSERT INTO `stufile` (`stuno`, `deptno`, `name`, `entryYear`, `gradDate`) VALUES
(1092955, 347, '馮昱銘', 110, NULL),
(1102911, 347, '黃梓壕', 110, NULL),
(1102912, 347, '林承翰', 110, NULL),
(1102913, 347, '王柏勝', 110, NULL),
(1102914, 347, '龔弋筠', 110, NULL),
(1102915, 347, '李嘉軒', 110, NULL),
(1102916, 347, '游雅碩', 110, NULL),
(1102917, 347, '王子奕', 110, NULL),
(1102918, 347, '蔡鎮謙', 110, NULL),
(1102919, 347, '楊溧佳', 110, NULL),
(1102920, 347, '陳柏凱', 110, NULL),
(1102921, 347, '林欣頤', 110, NULL),
(1102922, 347, '洪晨閔', 110, NULL),
(1102923, 347, '王柔綺', 110, NULL),
(1102924, 347, '李名智', 110, NULL),
(1102925, 347, '陳冠穎', 110, NULL),
(1102926, 347, '洪唯翔', 110, NULL),
(1102927, 347, '黃羿華', 110, NULL),
(1102928, 347, '林聖博', 110, NULL),
(1102929, 347, '湯惠元', 110, NULL),
(1102930, 347, '王成右', 110, NULL),
(1102931, 347, '謝茹琦', 110, NULL),
(1102932, 347, '林微訢', 110, NULL),
(1102933, 347, '郭秉蒝', 110, NULL),
(1102934, 347, '王雍心', 110, NULL),
(1102935, 347, '陳允瀚', 110, NULL),
(1102936, 347, '黃建澄', 110, NULL),
(1102937, 347, '余貫瑒', 110, NULL),
(1102938, 347, '潘泇樺', 110, NULL),
(1102939, 347, '林庭毅', 110, NULL),
(1102940, 347, '陳駿瑋', 110, NULL),
(1102941, 347, '林億安', 110, NULL),
(1102942, 347, '章駿睿', 110, NULL),
(1102943, 347, '顏莉諭', 110, NULL),
(1102944, 347, '林子揚', 110, NULL),
(1102945, 347, '謝孟翰', 110, NULL),
(1102946, 347, '蔡鈞宇', 110, NULL),
(1102947, 347, '呂沅芳', 110, NULL),
(1102948, 347, '吳祁叡', 110, NULL),
(1102949, 347, '陳柏佑', 110, NULL),
(1102950, 347, '龐宸業', 110, NULL),
(1102951, 347, '劉宗翰', 110, NULL),
(1102952, 347, '洪粲閎', 110, NULL),
(1102953, 347, '陳威凱', 110, NULL),
(1102956, 347, '陳為盛', 110, NULL),
(1102957, 347, '林琮穎', 110, NULL),
(1102958, 347, '林益新', 110, NULL),
(1102959, 347, '黃暄雨', 110, NULL),
(1102961, 347, '徐聖維', 110, NULL),
(1102962, 347, '鍾佳妘', 110, NULL),
(1102963, 347, '洪軍皓', 110, NULL),
(1102964, 347, '蔡仁翔', 110, NULL),
(1102965, 347, '張智堯', 110, NULL),
(1102966, 347, '邱翊銨', 110, NULL),
(1102967, 347, '鄭宇超', 110, NULL),
(1102970, 347, '賴貞蓉', 110, NULL);

-- --------------------------------------------------------

--
-- 資料表結構 `stu_course`
--

CREATE TABLE `stu_course` (
  `stuno` int(7) NOT NULL,
  `year` int(3) NOT NULL,
  `semester` int(1) NOT NULL,
  `stu_course_no` varchar(32) NOT NULL,
  `stu_course_score` decimal(5,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- 傾印資料表的資料 `stu_course`
--

INSERT INTO `stu_course` (`stuno`, `year`, `semester`, `stu_course_no`, `stu_course_score`) VALUES
(1102928, 110, 1, '34700010', 80.00),
(1102928, 110, 1, '34700025', 66.00),
(1102928, 110, 1, '34700031', 47.00),
(1102928, 110, 1, '34700032', 83.00),
(1102928, 110, 2, '34700015', 73.00),
(1102928, 110, 2, '34700017', 85.00),
(1102928, 110, 2, '34700022', 82.00),
(1102928, 110, 2, '34700023', 76.00),
(1102928, 110, 2, '34700024', 92.00),
(1102928, 110, 2, '34700153', 84.00),
(1102928, 111, 1, '34700016', 84.00),
(1102928, 111, 1, '34700029', 88.00),
(1102928, 111, 1, '34700038', 79.00),
(1102928, 111, 1, '34700039', 83.00),
(1102928, 111, 1, '34700042', 80.00),
(1102928, 111, 1, '34700055', 71.00),
(1102928, 111, 2, '34700036', 94.00),
(1102928, 111, 2, '34700037', 83.00),
(1102928, 111, 2, '34700041', 94.00),
(1102928, 111, 2, '34700058', 83.00),
(1102928, 111, 2, '34700065', 78.00),
(1102928, 111, 2, '34700107', 84.00);

--
-- 已傾印資料表的索引
--

--
-- 資料表索引 `accumulate_info`
--
ALTER TABLE `accumulate_info`
  ADD PRIMARY KEY (`stuno`);

--
-- 資料表索引 `course`
--
ALTER TABLE `course`
  ADD PRIMARY KEY (`cono`);

--
-- 資料表索引 `pubdep`
--
ALTER TABLE `pubdep`
  ADD PRIMARY KEY (`deptno`);

--
-- 資料表索引 `semester`
--
ALTER TABLE `semester`
  ADD PRIMARY KEY (`stuno`,`year`,`semester`);

--
-- 資料表索引 `stufile`
--
ALTER TABLE `stufile`
  ADD PRIMARY KEY (`stuno`);

--
-- 資料表索引 `stu_course`
--
ALTER TABLE `stu_course`
  ADD PRIMARY KEY (`stuno`,`year`,`semester`,`stu_course_no`);

--
-- 已傾印資料表的限制式
--

--
-- 資料表的限制式 `accumulate_info`
--
ALTER TABLE `accumulate_info`
  ADD CONSTRAINT `accumulate_info_ibfk_1` FOREIGN KEY (`stuno`) REFERENCES `stufile` (`stuno`);

--
-- 資料表的限制式 `semester`
--
ALTER TABLE `semester`
  ADD CONSTRAINT `semester_ibfk_1` FOREIGN KEY (`stuno`) REFERENCES `stufile` (`stuno`);

--
-- 資料表的限制式 `stu_course`
--
ALTER TABLE `stu_course`
  ADD CONSTRAINT `stu_course_ibfk_1` FOREIGN KEY (`stuno`) REFERENCES `stufile` (`stuno`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
