SELECT id AS "Id",
       verification_code AS "VerificationCode",
       full_name AS "FullName",
       is_admin AS "IsAdmin"
FROM users
WHERE id = any(@Ids)