-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Nov 13, 2025 at 05:30 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `payroll_system`
--

DELIMITER $$
--
-- Procedures
--
CREATE DEFINER=`root`@`localhost` PROCEDURE `AddEmployee` (IN `empNumber` VARCHAR(50), IN `lastName` VARCHAR(50), IN `firstName` VARCHAR(50), IN `middleName` VARCHAR(50), IN `dob` DATE, IN `dailyRate` DECIMAL(10,2), IN `workingDays` ENUM('MWF','TTHS'))   BEGIN
    INSERT INTO Employees(EmployeeNumber, LastName, FirstName, MiddleName, DateOfBirth, DailyRate, WorkingDays)
    VALUES(empNumber, lastName, firstName, middleName, dob, dailyRate, workingDays);
END$$

CREATE DEFINER=`root`@`localhost` PROCEDURE `GetAllEmployees` ()   BEGIN
    SELECT * FROM Employees;
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `employees`
--

CREATE TABLE `employees` (
  `Id` int(11) NOT NULL,
  `EmployeeNumber` varchar(50) NOT NULL,
  `LastName` varchar(50) NOT NULL,
  `FirstName` varchar(50) NOT NULL,
  `MiddleName` varchar(50) DEFAULT NULL,
  `DateOfBirth` date NOT NULL,
  `DailyRate` decimal(10,2) NOT NULL,
  `WorkingDays` enum('MWF','TTHS') NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `employees`
--

INSERT INTO `employees` (`Id`, `EmployeeNumber`, `LastName`, `FirstName`, `MiddleName`, `DateOfBirth`, `DailyRate`, `WorkingDays`) VALUES
(4, 'MOR-26222-13NOV2025', 'morando', 'mark daniel', 'mampusti', '2025-11-13', 2000.00, 'TTHS'),
(5, 'MOR-05840-11NOV2025', 'Morando', 'Mark Danail', '', '2025-11-11', 500.00, 'MWF'),
(6, 'MOR-54474-24APR2002', 'morando', 'mark daniel', 'dsa', '2002-04-24', 200.00, 'MWF'),
(7, 'ADM-42284-14APR2022', 'ad', 'mark daniel', 'das', '2022-04-14', 300.00, 'TTHS'),
(18, 'REY-49712-05SEP2002', 'Reyes', 'Ampuy', '', '2002-09-05', 700.00, 'TTHS');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `employees`
--
ALTER TABLE `employees`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `employees`
--
ALTER TABLE `employees`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
