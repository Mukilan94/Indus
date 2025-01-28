jQuery(document).ready(function (){

    //it makes clickable of root level of multilevel elements in sidebar
    jQuery('.navtext').on('click', function (e) {
        window.location = jQuery(e.target).parent().attr('href');
    });
});