DROP VIEW
IF EXISTS customer.vwsupplier;

CREATE VIEW customer.vwsupplier AS SELECT
	s.*, C . NAME country_name,
	COALESCE (sa.agent_count, 0) agent_count,
	COALESCE (sp.product_count, 0) product_count
FROM
	customer.supplier s
JOIN common.address A ON s.address_id = A . ID
JOIN common.country C ON A .country_id = C . ID
LEFT JOIN (
	SELECT
		supplier_id,
		"count" (*) agent_count
	FROM
		customer.agent_supplier
	WHERE
		is_active = 't'
	GROUP BY
		supplier_id
) sa ON s. ID = sa.supplier_id
LEFT JOIN (
	SELECT
		supplier_id,
		"count" (*) product_count
	FROM
		commodity.supplier_product
	WHERE
		is_active = 't'
	GROUP BY
		supplier_id
) sp ON s. ID = sp.supplier_id