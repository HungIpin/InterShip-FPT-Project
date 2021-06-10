$(function() {

    //Wait for Pinegrow to wake-up
    $("body").one("pinegrow-ready", function(e, pinegrow) {

        //Create new Pinegrow framework object
        var f = new PgFramework("C__Users_lamgi_Downloads_popup-khoi - Copy (2)_angular-template_UserLib", "C__Users_lamgi_Downloads_popup-khoi - Copy (2)_angular-template_UserLib");

        //This will prevent activating multiple versions of this framework being loaded
        f.type = "C__Users_lamgi_Downloads_popup-khoi - Copy (2)_angular-template_UserLib";
        f.allow_single_type = true;
        f.user_lib = true

        var comp_comp1 = new PgComponentType('comp1', 'Comp 1 / Div');
        comp_comp1.code = '<div class="col-md-4">\
    <h2>Certificate</h2>\
    <p>Get your desired certificate by passing the required grade </p>\
    <p><a class="btn text-white" style="background-color:#17a2b8" href="#/certifications" role="button">View details »</a></p>\
</div>';
        comp_comp1.parent_selector = '.row';
        f.addComponentType(comp_comp1);
        
        //Tell Pinegrow about the framework
        pinegrow.addFramework(f);
            
        var section = new PgFrameworkLibSection("C__Users_lamgi_Downloads_popup-khoi - Copy (2)_angular-template_UserLib_lib", "Components");
        //Pass components in array
        section.setComponentTypes([comp_comp1]);

        f.addLibSection(section);
   });
});