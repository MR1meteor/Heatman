SELECT b.id
FROM brigades b
         JOIN brigade_employees be ON be.brigade_id = b.id
WHERE b.creation_date BETWEEN @StartDate AND @EndDate
  AND be.employee_id = ANY(@EmployeeIds)
GROUP BY b.id
HAVING COUNT(DISTINCT be.employee_id) = ARRAY_LENGTH(@EmployeeIds, 1);