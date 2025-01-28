jQuery(document).ready(function (){

    //it makes clickable of root level of multilevel elements in sidebar
    jQuery('.navtext').on('click', function (e) {
        window.location = jQuery(e.target).parent().attr('href');
    })

    jQuery(".nav-left-close").click(function () {

        // Set the effect type
        var effect = 'slide';

        // Set the options for the effect type chosen
        var options = { direction: 'right' };

        // Set the duration (default: 400 milliseconds)
        var duration = 500;

        jQuery(".nav-notify-left").addClass('nav-notify-left-collapse');
        jQuery(".nav-notify-left-collapse").removeClass('nav-notify-left');
        jQuery('.nav-notify-right').hide();
        jQuery('.nav-notify-center').hide();
        jQuery(".nav-left-close").hide();
    });
    jQuery(".nav-left-bell").click(function () {

        // Set the effect type
        var effect = 'slide';

        // Set the options for the effect type chosen
        var options = { direction: 'right' };

        // Set the duration (default: 400 milliseconds)
        var duration = 500;

        jQuery(".nav-notify-left-collapse").addClass('nav-notify-left');
        jQuery(".nav-notify-left").removeClass('nav-notify-left-collapse');
        jQuery('.nav-notify-right').show();
        jQuery('.nav-notify-center').show();
        jQuery(".nav-left-close").show();
    });
});