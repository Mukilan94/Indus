var callStatus = $("#call-status");
var answerButton = $(".answer-button");
var callSupportButton = $(".call-support-button");
var hangUpButton = $(".hangup-button");
var callCustomerButtons = $(".call-customer-button");
/*var outgoingCallHangupButton = $(".button-hangup-outgoing");*/
var outgoingCallHangupButton = document.getElementById("btnHangUp");
var device;
var token;
var callingDeviceIdentity;

// Event Listeners

//callButton.onclick = (e) => {
//    e.preventDefault();
//    makeOutgoingCall();
//};

//getAudioDevicesButton.onclick = getAudioDevices;
//speakerDevices.addEventListener("change", updateOutputDevice);
//ringtoneDevices.addEventListener("change", updateRingtoneDevice);

// SETUP STEP 1:
// Browser client should be started after a user gesture
// to avoid errors in the browser console re: AudioContext
//startupButton.addEventListener("click", startupClient);
//startupClient();

// SETUP STEP 2: Request an Access Token
async function startupClient() {
    debugger;
    hangUpButton.hide();
    log("Requesting Access Token...");

    try {
        const data = await $.getJSON("/VoiceToken");
        log("Got a token.");

        token = data.token;
        callingDeviceIdentity = data.identity;
        //setClientNameUI(data.identity);
        intitializeDevice();
    } catch (err) {
        console.log(err);
        log("An error occurred. See your browser console for more information.");
    }
}

// SETUP STEP 3:
// Instantiate a new Twilio.Device
function intitializeDevice() {
    //logDiv.classList.remove("hide");
    log("Initializing device");
    debugger;
    device = new Twilio.Device(token, {
        logLevel: 1,
        answerOnBridge: true,
        // Set Opus as our preferred codec. Opus generally performs better, requiring less bandwidth and
        // providing better audio quality in restrained network conditions. Opus will be default in 2.0.
        codecPreferences: ["opus", "pcmu"],
    });

    addDeviceListeners(device);

    // Device must be registered in order to receive incoming calls
    device.register();
}

// SETUP STEP 4:
// Listen for Twilio.Device states
function addDeviceListeners(device) {
    device.on("registered", function () {
        log("Twilio.Device Ready to make and receive calls!");
        //callControlsDiv.classList.remove("hide");
    });

    device.on("error", function (error) {
        log("Twilio.Device Error: " + error.message);
    });

    //device.on("incoming", handleIncomingCall);

    //device.audio.on("deviceChange", updateAllAudioDevices.bind(device));

    // Show audio selection UI if it is supported by the browser.
    //if (device.audio.isOutputSelectionSupported) {
    //    //audioSelectionDiv.classList.remove("hide");
    //}
}

// MAKE AN OUTGOING CALL

async function callCustomer(phoneNumber) {


    //$.when(await startupClient()).then(function (data, textStatus, jqXHR) {
    await startupClient();
    const params = {
        To: phoneNumber,
        callingDeviceIdentity
    };
    if (device) {
        //log(`Attempting to call ${params.To} ...`);
        callStatus.text("Attempting to call" + params.To + "...");

        // Twilio.Device.connect() returns a Call object
        const call = await device.connect({ params });

        // add listeners to the Call
        // "accepted" means the call has finished connecting and the state is now "open"
        call.on("accept", updateUIAcceptedOutgoingCall);
        call.on("disconnect", updateUIDisconnectedOutgoingCall);
        call.on("cancel", updateUICancelOutgoingCall);
        call.on("reject", updateUIRejectOutgoingCall);

        outgoingCallHangupButton.onclick = () => {
            //log("Hanging up ...");
            call.disconnect();
        };

    } else {
        //log("Unable to make call.");
        callStatus.text("Unable to make call.");
    }
    //});


}
function hangUp() {
    //Twilio.Device.disconnectAll();
    call.disconnect();
}
function updateUIAcceptedOutgoingCall(call) {
    callStatus.text("Call in progress ...");
    hangUpButton.show();
    //callStatus.text(`Attempting to call ${params.To} ...`);
    //callButton.disabled = true;
    //outgoingCallHangupButton.classList.remove("hide");
    //volumeIndicators.classList.remove("hide");
    //bindVolumeIndicators(call);
}

function updateUIDisconnectedOutgoingCall() {
    callStatus.text("Call hung up...");
    hangUpButton.hide();
}

function updateUICancelOutgoingCall() {
    callStatus.text("Call Canceled...");
    hangUpButton.hide();
}

function updateUIRejectOutgoingCall() {
    callStatus.text("Call Rejected...");
    hangUpButton.hide();
}

// MISC USER INTERFACE

// Activity log
function log(message) {
    //logDiv.innerHTML += `<p class="log-entry">&gt;&nbsp; ${message} </p>`;
    //logDiv.scrollTop = logDiv.scrollHeight;
    console.log('Twilio console' + message);
}

