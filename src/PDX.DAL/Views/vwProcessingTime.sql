DROP VIEW
IF EXISTS procurement.vwprocessing_time;

CREATE VIEW procurement.vwprocessing_time AS SELECT
	ip.ID,
	ip.import_permit_number,
	COALESCE(lg.submission_date, ip.created_date) submission_date,
	ip.assigned_date,
	lg.submission_for_decision_date,
	lg.decision_date,
	DATE_PART(
		'day',
		lg.decision_date - lg.submission_date
	) * 24 + DATE_PART(
		'hour',
		lg.decision_date - lg.submission_date
	) AS processing_time_in_hour,
	DATE_PART(
		'day',
		decision_date - submission_date
	) AS processing_time_in_day
FROM
	procurement.import_permit ip
JOIN common.import_permit_status ips ON ip.import_permit_status_id = ips. ID
LEFT JOIN (
	SELECT
	import_permit_id,
	MAX (
		CASE
		WHEN to_status_code = 'RQST' THEN
			lg.modified_date
		END
	) AS "submission_date",
MAX (
		CASE
		WHEN to_status_code IN ('SRF','SFA') THEN
			lg.modified_date
		END
	) AS "submission_for_decision_date",
	MAX (
		CASE
		WHEN to_status_code IN ('APR', 'REJ') THEN
			lg.modified_date
		END
	) AS "decision_date"
FROM
	procurement.vwimport_permit_log_status lg
WHERE to_status_code IN ('RQST','SRF','SFA', 'APR', 'REJ')
GROUP BY
	import_permit_id) as lg on ip.id = lg.import_permit_id