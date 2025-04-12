SELECT b.id
FROM brigades b
         JOIN brigade_employees be ON be.brigade_id = b.id
WHERE b.creation_date BETWEEN @StartDate AND @EndDate
GROUP BY b.id
HAVING COUNT(DISTINCT CASE WHEN be.employee_id = any(@EmployeeIds) THEN be.employee_id END) = (SELECT COUNT(*) FROM STRING_SPLIT(@EmployeeIds, ','));