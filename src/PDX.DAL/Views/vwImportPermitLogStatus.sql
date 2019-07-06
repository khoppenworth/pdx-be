DROP VIEW
IF EXISTS procurement.vwimport_permit_log_status;

CREATE VIEW procurement.vwimport_permit_log_status AS SELECT
	ils.*, ip.import_permit_number,
	fStatus. NAME AS "from_status_name",
	fStatus.import_permit_status_code AS "from_status_code",
	tStatus. NAME AS to_status_name,
	tStatus.import_permit_status_code AS to_status_code,
	us.first_name || ' ' || us.last_name AS modified_by
FROM
	procurement.import_permit_log_status ils
JOIN procurement.import_permit ip ON ils.import_permit_id = ip."id"
LEFT JOIN common.import_permit_status fStatus ON ils.from_status_id = fStatus."id"
JOIN common.import_permit_status tStatus ON ils.to_status_id = tStatus. ID
JOIN account. USER us ON ils.modified_by_user_id = us. ID