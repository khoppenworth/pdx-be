DROP VIEW
IF EXISTS procurement.vwpip;

CREATE VIEW procurement.vwpip AS SELECT
	ip.*, ag. NAME agent_name,
	s. NAME supplier_name,
	pot. NAME port_of_entry,
	pom. NAME payment_mode,
	sm. NAME shipping_method,
	cu. NAME currency,
	ips. NAME import_permit_status,
	ips.short_name import_permit_status_sh,
	ips.priority import_permit_status_priority,
	ips.display_name import_permit_status_display_name,
	ips.import_permit_status_code,
	pot.short_name port_of_entry_sh,
	pom.short_name payment_mode_sh,
	sm.short_name shipping_method_sh,
	cu.short_name currency_sh,
	cu.symbol currency_symbol,
	us.user_name created_by_username,
	aus.first_name || ' ' || aus.last_name AS assigned_user,
	pt.submission_date,
	pt.submission_for_decision_date,
	pt.decision_date
FROM
	procurement.import_permit ip
JOIN customer.agent ag ON ip.agent_id = ag. ID
JOIN customer.supplier s ON ip.supplier_id = s. ID
JOIN common.port_of_entry pot ON ip.port_of_entry_id = pot. ID
JOIN common.payment_mode pom ON ip.payment_mode_id = pom. ID
JOIN common.shipping_method sm ON ip.shipping_method_id = sm. ID
JOIN common.currency cu ON ip.currency_id = cu. ID
JOIN common.import_permit_status ips ON ip.import_permit_status_id = ips. ID
JOIN common.import_permit_type ipt ON ip.import_permit_type_id = ipt. ID
JOIN account. USER us ON ip.created_by_user_id = us. ID
LEFT JOIN account. USER aus ON assigned_user_id = aus. ID
LEFT JOIN procurement.vwprocessing_time pt on ip.id = pt.id
WHERE ipt.import_permit_type_code = 'PIP';