DROP VIEW
IF EXISTS commodity.vwproduct;

CREATE VIEW commodity.vwproduct AS 
-- SELECT sp.*, s."name" supplier_name,
-- 	asu.agent_name,
-- 	M . NAME manufacturer_name,
-- 	C . NAME country_name,
-- 	P . NAME brand_name,
-- 	P .generic_name || ' - ' || P .dosage_strength || ' ' || P .dosage_unit || ' - ' || P .dosage_form AS full_item_name,
-- 	P .shelf_life,
-- 	ct. NAME commodity_type_name
-- FROM
-- 	commodity.supplier_product sp
-- JOIN commodity.product P ON sp.product_id = P ."id"
-- JOIN common.commodity_type ct ON P .commodity_type_id = ct. ID
-- JOIN customer.supplier s ON sp.supplier_id = s."id"
-- LEFT JOIN (
-- 	SELECT
-- 		asu.supplier_id,
-- 		ag. NAME agent_name
-- 	FROM
-- 		customer.agent_supplier asu
-- 	JOIN common.agent_level al ON asu.agent_level_id = al. ID
-- 	JOIN customer.agent ag ON asu.agent_id = ag. ID
-- 	WHERE
-- 		al.agent_level_code = 'FAG'
-- ) asu ON s. ID = asu.supplier_id
-- LEFT JOIN (
-- 	SELECT
-- 		product_id,
-- 		MAX (M . ID) max_manufacturer_id
-- 	FROM
-- 		commodity.product_manufacturer pm
-- 	JOIN customer.manufacturer M ON pm.manufacturer_id = M . ID
-- 	GROUP BY
-- 		product_id
-- 	ORDER BY
-- 		product_id
-- ) pm ON P . ID = pm.product_id
-- LEFT JOIN customer.manufacturer M ON pm.max_manufacturer_id = M . ID
-- LEFT JOIN common.country C ON M .country_id = C . ID
-- Where sp.expiry_date is not null AND sp.expiry_date > now()::date
 SELECT sp.id,
    sp.created_date,
    sp.is_active,
    sp.modified_date,
    sp.product_id,
    sp.rowguid,
    sp.supplier_id,
    asu.agent_id,
    sp.expiry_date,
    sp.registration_date,
    s.name AS supplier_name,
    asu.agent_name,
    m.name AS manufacturer_name,
    c.name AS country_name,
    p.name AS brand_name,
    (((((((p.generic_name)::text || ' - '::text) || COALESCE(dst.name, ' '::text)) || ' '::text) || COALESCE(du.name, ' '::text)) || ' - '::text) || COALESCE(ds.name, ' '::text)) AS full_item_name,
    p.shelf_life,
    ct.name AS commodity_type_name
   FROM ((((((((((commodity.supplier_product sp
     JOIN commodity.product p ON ((sp.product_id = p.id)))
     LEFT JOIN commodity.dosage_form ds ON ((ds.id = p.dosage_form_id)))
     LEFT JOIN commodity.dosage_unit du ON ((du.id = p.dosage_unit_id)))
     LEFT JOIN commodity.dosage_strength dst ON ((dst.id = p.dosage_strength_id)))
     JOIN common.commodity_type ct ON ((p.commodity_type_id = ct.id)))
     JOIN customer.supplier s ON ((sp.supplier_id = s.id)))
     LEFT JOIN ( SELECT asu_1.supplier_id,
            ag.id AS agent_id,
            ag.name AS agent_name
           FROM ((customer.agent_supplier asu_1
             JOIN common.agent_level al ON ((asu_1.agent_level_id = al.id)))
             JOIN customer.agent ag ON ((asu_1.agent_id = ag.id)))
          WHERE (al.agent_level_code = 'FAG'::text)) asu ON ((s.id = asu.supplier_id)))
     LEFT JOIN ( SELECT pm_1.product_id,
            max(m_1.id) AS max_manufacturer_id
           FROM (commodity.product_manufacturer pm_1
             JOIN customer.manufacturer m_1 ON ((pm_1.manufacturer_id = m_1.id)))
          GROUP BY pm_1.product_id
          ORDER BY pm_1.product_id) pm ON ((p.id = pm.product_id)))
     LEFT JOIN customer.manufacturer m ON ((pm.max_manufacturer_id = m.id)))
     LEFT JOIN common.country c ON ((m.country_id = c.id)))
  WHERE ((sp.expiry_date IS NOT NULL) AND (sp.expiry_date > (now())::date));