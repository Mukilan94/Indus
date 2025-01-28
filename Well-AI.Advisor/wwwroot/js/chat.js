//Karthik
//var chatClient;
//var userChannel;
//var invitedChannel;
//var loginUserId;
//var currentChatUserId;
//var selectedProfile;

var MESSAGES_HISTORY_LIMIT = 50;

//Karthik
//var chatClientJoinUserChannel
//var userChannel;
//var invitedChannel;
//var loginUserId;
//var currentChatUserId;
//var selectedProfile;
var messagebody = '';

function LeaveUserChannel(userId) {
    if (userChannel) {
        userChannel.leave().then(function (leftChannel) {
            console.log('left ' + leftChannel.friendlyName);
            $("#li_" + userId).remove();
            //leftChannel.removeListener('messageAdded', tc.addMessageToList);
            //leftChannel.removeListener('typingStarted', showTypingStarted);
            //leftChannel.removeListener('typingEnded', hideTypingStarted);
            //leftChannel.removeListener('memberJoined', notifyMemberJoined);
            //leftChannel.removeListener('memberLeft', notifyMemberLeft);
            return true;
        }).catch(function () {
            return false;
        });
    }
    else {
        return Promise.resolve();
    }

}
//userChannel
function JoinUserChannel(channel, inviteuser, primaryuser) {

 
    console.log('Join User Channel Called : ' + channel.friendlyName);
    chatClient.getConversationByUniqueName(channel.friendlyName)
        .then(function (channel) {
            userChannel = channel;
            console.log('Found channel:');
            //console.log(generalChannel);
            //InviteNewUserToChannel(channel, inviteuser);
            setupUserChannel();
        }).catch(function () {
            console.log('Creating user channel' + channel.friendlyName);
            chatClient.createConversation({
                uniqueName: channel.friendlyName,
                friendlyName: channel.friendlyName,
                isPrivate: true
            }).then(function (channel) {
                console.log('Created user channel:' + channel.friendlyName);
                console.log(channel);
                userChannel = channel;
                //saveChannelInTwilioUserMapping(selectedProfile, userChannel.sid, userChannel.createdBy, userChannel.friendlyName);               

                $.when(saveChannelInTwilioUserMapping(selectedProfile, userChannel.sid, userChannel.createdBy, userChannel.friendlyName)).then(function (data, textStatus, jqXHR) {
                    //userChannelCustomer = channel;
                    //InviteNewUserToChannelCustomer(channel, inviteuser);
                    setupUserChannelCustomer();
                });
                //InviteNewUserToChannel(channel, inviteuser);
                setupUserChannel();
            }).catch(function (channel) {
                console.log('Channel could not be created:');
                console.log(channel);
                setupUserChannel();
            });
        });
}
function CreateUserChannel(fromAndToUserUniqueName, inviteuser) {
    console.log('Create User Channel Called : ' + fromAndToUserUniqueName);
    chatClient.getConversationByUniqueName(fromAndToUserUniqueName)
        .then(function (channel) {
            userChannel = channel;
            console.log('Found user channel:');
            console.log(userChannel);
            //InviteNewUserToChannel(channel, inviteuser);
            setupUserChannel();
        }).catch(function () {
            // If it doesn't exist, let's create it
            //console.log('Creating user channel' + channel.friendlyName);
            chatClient.createConversation({
                uniqueName: fromAndToUserUniqueName,
                friendlyName: fromAndToUserUniqueName,
                isPrivate: true
            }).then(function (channel) {
                console.log('Created user channel:' + channel.friendlyName);
                //console.log('Created user channel:' + channel.);
                console.log(channel);
                $.when(saveChannelInTwilioUserMapping(currentChatUserId, channel.sid, channel.createdBy, channel.friendlyName)).then(function (data, textStatus, jqXHR) {
                    userChannelCustomer = channel;
                    //InviteNewUserToChannelCustomer(channel, inviteuser);
                    setupUserChannelCustomer();
                });
             
                userChannel = channel;
                //InviteNewUserToChannel(channel, inviteuser);
                //setupUserChannel();
            }).catch(function (channel) {
                //debugger;
                console.log('Channel could not be created:');
                //InviteNewUserToChannel(channel, inviteuser);
                console.log(channel);
            });
        });
}
function InviteNewUserToChannel(channel, inviteuser) {
   
    //chat.view.element[0].childNodes[0].innerHTML = "";
    // Invite another member to your channel
    var inviteduser = inviteuser;
    // Invite another member to your channel
    channel.add(inviteduser).then(function () {
        console.log('Your friend (Selected User) has been invited! ' + inviteuser);
        inviteUsertoChannel(channel.sid, inviteuser);


    }).catch(function (channel) {
        console.log('User already invited');
        console.log(channel);
    });
}

function setupUserChannel() {
    console.log('setupUserChannel :');

    //chat.view.element[0].childNodes[0].innerHTML = "";
    if (chat != undefined) {
        chat.view.element[0].childNodes[0].innerHTML = "";
    }
    //if (chatCustomer != undefined) {
    //    chatCustomer.view.element[0].childNodes[0].innerHTML = "";
    //}
    // Join the general channel
    if (userChannel.channelState.status !== "joined") {
     
        userChannel.join().then(function (channel) {
        });
    }

    userChannel.on('messageAdded', function (message) {
        if (messagebody != message.body) {
            console.log('Message added event call at ServiceCompany :');
            var fromUser = message.author;
            var userInfo = {
                name: fromUser
            };

            //var d = new Date(message.dateUpdated);

            // d = d.getFullYear() + "-" + ('0' + (d.getMonth() + 1)).slice(-2) + "-" + ('0' + d.getDate()).slice(-2) + " " + ('0' + d.getHours()).slice(-2) + ":" + ('0' + d.getMinutes()).slice(-2);


            if (fromUser !== loginUserId) {
                userInfo.name = selectedProfile;
                //chat.renderMessage({ type: "text", text: message.body }, userInfo);
                if (chat != undefined) {
                    chat.renderMessage({ type: "text", text: message.body }, userInfo);
                }
               
            }
            messagebody = message.body;
        }
      
    });
    
    //chat.view.element[0].childNodes[0].innerHTML = "";
    //chat.view.element[0].childNodes[0].innerHTML = "";

    if (chat != undefined) {
        chat.view.element[0].childNodes[0].innerHTML = "";
    }
 

    userChannel.getMessages(MESSAGES_HISTORY_LIMIT).then(function (messages) {
        const totalMessages = messages.items.length;
        //chat.element.remove();
        for (i = 0; i < totalMessages; i++) {
            const message = messages.items[i];

            var fromUser = message.author;
            var timestamp = message.dateUpdated;
            var userInfo = {
                name: fromUser
            };

            var d = new Date(message.dateUpdated);

            d = d.getFullYear() + "-" + ('0' + (d.getMonth() + 1)).slice(-2) + "-" + ('0' + d.getDate()).slice(-2) + " " + ('0' + d.getHours()).slice(-2) + ":" + ('0' + d.getMinutes()).slice(-2);

            console.log('loginUserId at render message :' + message.author);
            console.log('Author at render message :' + message.author);
            if (loginUserId == fromUser) {

                //chat.userName = "Me" + ', ' + d.toString();
                
                if (chat != undefined) {
                    chat.renderMessage({ type: "text", text: message.body, timestamp: d.toString() }, chat.getUser());
                }
                //if (chatCustomer != undefined) {
                //    chatCustomer.renderMessage({ type: "text", text: message.body, timestamp: d.toString() }, chatCustomer.getUser());
                //}
            }
            else {
                userInfo.name = selectedProfile;
                //chat.renderMessage({ type: "text", text: message.body, timestamp: d.toString() }, userInfo);
                if (chat != undefined) {
                    chat.renderMessage({ type: "text", text: message.body, timestamp: d.toString() }, userInfo);
                }
                //if (chatCustomer != undefined) {
                //    chatCustomer.renderMessage({ type: "text", text: message.body, timestamp: d.toString() }, userInfo);
                //}
            }
        }
        console.log('Total Messages:' + totalMessages);

        //if (fromUser1 !== loginUserId) {
    });

}
var currentMessage = '';
function onPost(args) {

    console.log('sendMessage args' + args);
    console.log();
    //generalChannel.sendMessage(args.text);
    //const msg = $('#chat-input').val();

    userChannel.sendMessage(args.text);

    //Check user is online if not then we will store count of messages into database
    //Message Type - Chat/Video Call
    SaveCommunicationNotification(currentChatUserId, args.text,'Chat Message');

    InviteNewUserToChannel(userChannel, currentChatUserId);//, user.identity, userChannel.friendlyName);
    //add users to twiliouserchatmappings table if channel is not there.
}

