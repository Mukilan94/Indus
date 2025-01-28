"use strict";

var chatHub = new signalR.HubConnectionBuilder()
	.withUrl("/notificationHub")
	.build();


function onConnected(connection) {

	console.log('connection started');
	connection.on('updateNotification', function () {
		console.log('messageCallback1');

		 
	});
	 
};


chatHub.start()
	.then(function () {
		 
		console.log('Notification JS Start Function');
		onConnected(chatHub);

	})
	.catch(function (error) {
		console.error(error.message);
		 
	});





