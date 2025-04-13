SELECT id as "Id",
       verification_code as "VerificationCode"
FROM users
WHERE verification_code = @VerificationCode