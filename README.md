# Payroll System
Created By Mark Daniel Morando - Run this for my UI https://localhost:7124/

## Overview
This is a simple Payroll System built using HTML, CSS, JavaScript (frontend) and .NET (C#) Web API for the backend. The system allows users to manage employees, including adding, updating, deleting, querying employee data, and computing take-home pay based on working days and daily rate.

---------------------------------------------------------------------------------------------------------------

## Features
1. Add Employee
   - Input first name, last name, middle name, date of birth, daily rate, and working days.  
   - Employee number is **automatically generated** using the first three letters of the name, a 5-digit random number, and the employee's date of birth.  
   - Saves employee data to the backend database.

2. Update Employee
   - Edit employee details except for the employee number (it remains auto-generated).  
   - Updates data in the database via API.

3. Delete Employee
   - Remove an employee from the system.  
   - Confirms deletion before removing.
   - 
4. Query Employees
   - Displays a table of all employees.  
   - Table is scrollable if there are more than 5 employees to maintain a clean layout.  
   - Table includes ID, Employee Number, Name, Daily Rate, Working Days, and action buttons (Edit/Delete).

5. Compute Take-Home Pay 
   - Input employee ID, start date, and end date.  
   - Calculates total pay based on working days and daily rate.  
   - Shows the result dynamically below the form.

---------------------------------------------------------------------------------------------------------------

## How it Works
### Frontend (HTML/JS/CSS)
- HTML: Provides forms for adding/editing employees and computing pay, and a table to display employee data.  
- CSS: Styled for modern aesthetic using Google Fonts, soft shadows, rounded corners, and hover effects.  
- **JavaScript**:  
  - Fetches employee data from the API (`GET /api/Employees`).  
  - Sends new employee data (`POST /api/Employees`) and updates (`PUT /api/Employees/{id}`).  
  - Deletes employees (`DELETE /api/Employees/{id}`).  
  - Sends compute requests (`POST /api/Employees/compute`) and displays take-home pay.

### Backend (C# .NET API)
- Employee Model: Stores employee data including auto-generated Employee Number.  
- Endpoints:
  - `GET /api/Employees` → Fetch all employees.  
  - `GET /api/Employees/{id}` → Fetch a single employee.  
  - `POST /api/Employees` → Create a new employee (generates employee number).  
  - `PUT /api/Employees/{id}` → Update employee data.  
  - `DELETE /api/Employees/{id}` → Delete an employee.  
  - `POST /api/Employees/compute` → Calculate take-home pay for a date range.

### Employee Number Generation
- Format: `{First3LettersOfLastName}-{Random5Digit}-{ddMMMyyyy}`  
- Example: `DEL-12340-10JAN1994`

### Take-Home Pay Calculation
- Computes pay by counting the number of working days in the selected range.  
- Birthday pay is included if the employee’s birthday falls within the period.  
- Pay = `Daily Rate x Number of Working Days`.

---------------------------------------------------------------------------------------------------------------

## Demonstration
1. Add a new employee using the form.  
2. Edit employee details via the table’s **Edit** button.  
3. Delete an employee via the table’s **Delete** button.  
4. Compute take-home pay using the **Compute Take Home Pay** form.  
5. Table is scrollable if there are more than 5 employees, keeping the UI clean.

---------------------------------------------------------------------------------------------------------------

## GitHub Repository
https://github.com/Niyelz/PayrollSystem


