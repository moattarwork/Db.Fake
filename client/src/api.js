const baseApiUrl = 'https://localhost:5002';

export async function loadDatabases(connection) {
	const url = `${baseApiUrl}/Schema?connectionString=${encodeURI(connection)}`;
	const response = await fetch(url);
	return await response.json();
}

export async function loadTables(connection, database) {
	const url = `${baseApiUrl}/Schema/for/${encodeURI(
		database
	)}?connectionString=${encodeURI(connection)}`;
	const response = await fetch(url);
	return await response.json();
}

export async function exportPlan(connection, database, maskedFields) {
	const url = `${baseApiUrl}/schema/export`;
	const payload = {
		connection: connection,
		database: database,
		maskedFields: maskedFields
	};

	const response = await fetch(url, {
		method: 'post',
		//mode: 'cors',
		headers: {
			'content-type': 'application/json'
		},
		body: JSON.stringify(payload)
	});

	const filename = response.headers.get('x-filename');
	const blob = await response.blob();

	downloadBlob(blob, filename);
}

function downloadBlob(blob, filename) {
	const blobUrl = window.URL.createObjectURL(blob);

	const a = document.createElement('a');
	document.body.appendChild(a);
	a.style = 'display: none';
	a.href = blobUrl;
	a.download = filename ? filename : 'download.json';
	a.click();
	window.URL.revokeObjectURL(blobUrl);
}
