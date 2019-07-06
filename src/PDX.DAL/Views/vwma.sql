DROP VIEW
IF EXISTS license.vwma;

CREATE VIEW license.vwma AS SELECT
	ma.*, ag. NAME agent_name,
	sup. NAME supplier_name,
	mas. NAME ma_status,
	mas.priority ma_status_priority,
	mas.display_name ma_status_display_name,
	mas.ma_status_code,
	mat."name" AS ma_type,
	mat.ma_type_code,
	DATE_PART('day', now() - ma.expiry_date)::int4 AS expiry_days
FROM
	license.ma AS ma
JOIN customer.agent ag ON ma.agent_id = ag."id"
JOIN customer.supplier sup ON ma.supplier_id = sup."id"
JOIN common.ma_status AS mas ON ma.ma_status_id = mas."id"
JOIN common.ma_type AS mat ON ma.ma_type_id = mat."id";

