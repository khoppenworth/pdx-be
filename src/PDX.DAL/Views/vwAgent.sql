DROP VIEW
IF EXISTS customer.vwagent;

CREATE VIEW customer.vwagent AS SELECT
	A .*, agt. NAME agent_type_name,
	C . NAME country_name
FROM
	customer.agent A
JOIN common.agent_type agt ON A .agent_type_id = agt."id"
JOIN common.address addr ON A .address_id = addr. ID
JOIN common.country C ON addr.country_id = C . ID