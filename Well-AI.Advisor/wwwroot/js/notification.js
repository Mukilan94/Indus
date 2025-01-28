"use strict";

var chatHub = new signalR.HubConnectionBuilder()
	.withUrl("/notificationHub")
	.build();

function onConnected(connection) {
 
    console.log('connection started');
	connection.on('updateNotification', function () {
		console.log('messageCallback1');
		
		updatenotification();
    });
	function updatenotification() {
		
		$.ajax({
			url: '/Identity/Account/Login?handler=NotificationCount',
			type: 'GET',
			success: function (data) {
				
				var MessageCount = data.messageValue /*+ ' Notifications'*/;
				var callCount = data.callValue /*+ ' Missed Calls'*/;
				console.log(MessageCount);
				
				$('#CallCount').text(callCount);
				
				$('#messageCount').text(MessageCount);
				console.log('Update Completed');
			}
		});

	}
};


chatHub.start()
	.then(function () {
		console.log('Notification JS Start Function');
        onConnected(chatHub);
       
    })
    .catch(function (error) {
        console.error(error.message);
	});

	



