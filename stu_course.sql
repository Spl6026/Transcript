-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- 主機： 127.0.0.1
-- 產生時間： 2024-01-03 21:06:54
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
(1102928,110,1,'34700031',47),
(1102928,110,2,'34700015',73),
(1102928,110,1,'34700032',83),
(1102928,110,1,'34700010',80),
(1102928,110,1,'34700025',66),
(1102928,110,2,'34700017',85),
(1102928,110,2,'34700022',82),
(1102928,111,1,'34700055',71),
(1102928,111,1,'34700016',84),
(1102928,111,2,'34700037',83),
(1102928,111,2,'34700036',94),
(1102928,110,2,'34700023',76),
(1102928,110,2,'34700024',92),
(1102928,111,1,'34700038',79),
(1102928,111,1,'34700039',83),
(1102928,111,2,'34700058',83),
(1102928,111,2,'34700107',84),
(1102928,110,2,'34700153',84),
(1102928,111,1,'34700029',88),
(1102928,111,2,'34700041',94),
(1102928,111,1,'34700042',80),
(1102928,111,2,'34700065',78);

--
-- 已傾印資料表的索引
--

--
-- 資料表索引 `stu_course`
--
ALTER TABLE `stu_course`
  ADD PRIMARY KEY (`stuno`,`year`,`semester`,`stu_course_no`);

--
-- 已傾印資料表的限制式
--

--
-- 資料表的限制式 `stu_course`
--
ALTER TABLE `stu_course`
  ADD CONSTRAINT `stu_course_ibfk_1` FOREIGN KEY (`stuno`) REFERENCES `stufile` (`stuno`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
