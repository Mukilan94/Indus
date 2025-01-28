var activeRoom;
var previewTracks;
var identity;
var roomName;

function attachTracks(tracks, container) {
    tracks.forEach(function (track) {
        container.appendChild(track.attach());
    });
}

function attachTrack(track, container) {
    //tracks.forEach(function (track) {
    container.appendChild(track.attach());
    //});
}

$(document).ready(function () {
    getToken();
    $("#button-join").click(function () {
        //joinRoom();
        console.log('Join room clicked');
    });
    // Bind button to leave room
    $('#button-end').click(function () {
        console.log('Leaving room...');
        if (activeRoom) {

            activeRoom.disconnect();

            $('#button-end').hide();
        }
    });
    $('#button-preview').click(function () {
        localVideoPreview();
    });
});

function getToken() {
    $.post("/Token/GenerateVideoToken", {}, function (data) {
        identity = data.identity;
        token = data.token;
        //  roomName = data.room;
    });
}
function joinRoom(room) {
    roomName = room;
    if (roomName) {
        console.log("Joining room '" + roomName + "'...");

        var connectOptions = { name: roomName };
        if (previewTracks) {
            connectOptions.tracks = previewTracks;
        }

        Twilio.Video.connect(token, connectOptions).then(roomJoined, function (error) {
            console.log('Could not connect to Twilio: ' + error.message);
        });
    } else {
        console.log('Please enter a room name.');
    }
}


function trackPublished(publication, participant) {
    //attachTracks(publication.track, previewContainer);
    var previewContainer = $('#local-media')[0];
    if (!previewContainer.querySelector('video')) {
        //attachParticipantTracks(room.localParticipant, previewContainer);
        //attachTracks(publication.track, previewContainer);
        attachTrack(publication.track, previewContainer);
    }


    publication.on('subscribed', track => {
        console.log(`LocalParticipant subscribed to a RemoteTrack: ${track}`);
        //assert(publication.isSubscribed);
        //assert(publication.track, track);
        var remoteContainer = $('#remote-media')[0];
        if (!remoteContainer.querySelector('video')) {
            attachTrack(track, previewContainer);
        }
    });

    publication.on('unsubscribed', track => {
        console.log(`LocalParticipant unsubscribed from a RemoteTrack: ${track}`);
        //assert(!publication.isSubscribed);
        //assert.equal(publication.track, null);
    });
}

function attachAttachableTracksForRemoteParticipant(participant) {
    participant.tracks.forEach(publication => {
        if (!publication.isSubscribed)
            return;

        //if (!trackExistsAndIsAttachable(publication.track))
        //return;

        var previewContainer = $('#remote-media')[0];;
        attachTrack(publication.track, previewContainer);
    });
}


function participantConnected(participant) {

    //participant.on('trackSubscribed', function (track) {
    //    var previewContainer = $('#remote-media')[0];;
    //    //attachTracks([track], previewContainer[0]);
    //    attachTracks(participant.tracks, previewContainer);
    //});

    //participant.on('trackPublished', publication => {
    //    trackPublished(publication, participant);
    //});

    //participant.on('trackUnpublished', publication => {
    //    console.log(`RemoteParticipant ${participant.identity} unpublished a RemoteTrack: ${publication}`);
    //});
    manageTracksForRemoteParticipant(participant);

    participant.on('trackUnpublished', publication => {
        //console.log(`RemoteParticipant ${participant.identity} unpublished a RemoteTrack: ${publication}`);
        //alert('Remote Participant trackUnpublished');
    });

}

function manageTracksForRemoteParticipant(participant) {
    // Handle tracks that this participant has already published.
    attachAttachableTracksForRemoteParticipant(participant);

    // Handles tracks that this participant eventually publishes.
    participant.on('trackSubscribed', onTrackSubscribed);
    participant.on('trackUnsubscribed', onTrackUnsubscribed);
}
function onTrackSubscribed(track) {
    //if (!trackExistsAndIsAttachable(track))
    //    return;   
    //alert('Local Participant onTrackSubscribed');
    var previewContainer = $('#remote-media')[0];;

    attachTrack(track, previewContainer);
}

function onTrackUnsubscribed(track) {
    //if (trackExistsAndIsAttachable(track))
    //track.detach().forEach(element => element.remove());
    //alert('local participant unsubscribed');
    closeWindowsIfOpen();
}

function ParticipantDisconnected() {
    //alert('Remote Participant disconnected');
    //if (room) {
    //    room.disconnect();
    //}    
    setTimeout(function () {
        if (activeRoom) {
            activeRoom.disconnect();
        }

        closeWindowsIfOpen();
    }
        , 1000);
}

function roomJoined(room) {
    activeRoom = room;
    console.log("Joined as '" + identity + "'");
    $('#button-end').show();
    room.localParticipant.tracks.forEach(trackPublished);

    // Attach the remote tracks of participants already in the room.
    room.participants.forEach(
        participant => manageTracksForRemoteParticipant(participant)
    );

    //room.participants.forEach(participantConnected);
    //room.on('participantConnected', participantConnected);

    room.on('participantConnected', participantConnected);
    room.on('participantDisconnected', ParticipantDisconnected);
    //window.onbeforeunload = () => room.disconnect();
}

function leaveRoomIfJoined() {
    if (activeRoom) {
        activeRoom.disconnect();
    }
    //if (room) {
    //    room.disconnect();
    //}
    //var wnd1 = $("#VideoDetails").data("kendoWindow");
    //var wnd2 = $("#VideoDetailsOperatingCompany").data("kendoWindow");
    //var wnd3 = $("#VideoDetailsServiceCompany").data("kendoWindow");
    //if (wnd1 != undefined) {
    //    wnd1.close();
    //}
    //if (wnd2 != undefined) {
    //    wnd2.close();
    //}
    //if (wnd3 != undefined) {
    //    wnd3.close();
    //}

}
function closeWindowsIfOpen() {
    if ($("#answerAudio")[0] != undefined) {
        $("#answerAudio")[0].pause();
    }
    if ($("#answerAudioOperatingCompany")[0] != undefined) {
        $("#answerAudioOperatingCompany")[0].pause();
    }
    if ($("#answerAudioServiceCompany")[0] != undefined) {
        $("#answerAudioServiceCompany")[0].pause();
    }

    var wnd1 = $("#VideoDetails").data("kendoWindow");
    var wnd2 = $("#VideoDetailsOperatingCompany").data("kendoWindow");
    var wnd3 = $("#VideoDetailsServiceCompany").data("kendoWindow");

    var wnd4 = $("#AnswerCall").data("kendoWindow");
    var wnd5 = $("#AnswerCallOperatingCompany").data("kendoWindow");
    var wnd6 = $("#AnswerCallServiceCompany").data("kendoWindow");

    if (wnd1 != undefined) {
        wnd1.close();
    }
    if (wnd2 != undefined) {
        wnd2.close();
    }
    if (wnd3 != undefined) {
        wnd3.close();
    }
    if (wnd4 != undefined) {
        wnd4.close();
    }
    if (wnd5 != undefined) {
        wnd5.close();
    }
    if (wnd6 != undefined) {
        wnd6.close();
    }
}