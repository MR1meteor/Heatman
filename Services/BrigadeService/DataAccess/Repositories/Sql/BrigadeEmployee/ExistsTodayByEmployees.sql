SELECT
    CASE
        WHEN EXISTS (
            SELECT 1
            FROM brigade_employees be
            JOIN brigades b ON be.brigade_id = b.id
            WHERE b.creation_date BETWEEN @StartDate AND @EndDate AND be.employee_id = any(@EmployeeIds)
        )
        THEN CAST(1 AS BIT)
        ELSE CAST(0 AS BIT)
    END
