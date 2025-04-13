SELECT id AS "Id",
       name AS "Name",
       surname AS "Surname",
       patronymic AS "Patronymic"
FROM employees
WHERE id = @Id
       