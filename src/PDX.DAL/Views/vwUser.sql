DROP VIEW
IF EXISTS account.vwuser;

CREATE VIEW account.vwuser AS SELECT
	u.*, ut."name" user_type_name,
	ut.user_type_code,
	ua. NAME agent_name
FROM
	account. USER u
JOIN common.user_type ut ON u.user_type_id = ut."id"
LEFT JOIN (
	SELECT
		ua.user_id,
		A .*
	FROM
		account.user_agent ua
	JOIN customer.agent A ON ua.agent_id = A . ID
	WHERE
		ua.is_active = 't'
) ua ON u. ID = ua.user_id