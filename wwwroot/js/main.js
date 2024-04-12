
window.addEventListener("load", () => {
    // page loader
    document.querySelector(".js-page-loader").classList.add("fade-out");
    setTimeout(() => {
        document.querySelector(".js-page-loader").style.display = "none";
    }, 600);
});

// testimonial slider
function testimonialSlider() {
    const carouselOne = document.getElementById('carouselOne');
    if (carouselOne) { // if the element exists
        carouselOne.addEventListener('slid.bs.carousel', function (event) {
            const activeItem = event.target.querySelector(".active");
            document.querySelector(".js-testimonial-img").src = 
            activeItem.getAttribute("data-js-testimonial-img");
        });
    }
}
testimonialSlider();

/*
    Course preview video
*/

function coursePreviewVideo(){
    // console.log('hello');
    const coursePreviewModal = document.querySelector(".js-course-preview-modal");
    if(coursePreviewModal){
        coursePreviewModal.addEventListener("shown.bs.modal", function(){
            this.querySelector(".js-course-preview-video").play();
            this.querySelector(".js-course-preview-video").currentTime = 0;
        });
        coursePreviewModal.addEventListener("hide.bs.modal", function(){
            this.querySelector(".js-course-preview-video").pause();
        })
    }
}
coursePreviewVideo();

// header menu
function headerMenu() {
    const menu = document.querySelector(".js-header-menu"),
    backdrop = document.querySelector(".js-header-backdrop"),
    menuCollapseBrealpoit = 991;

    function toggleMenu() {
        menu.classList.toggle("open");
        backdrop.classList.toggle("active");
        document.body.classList.toggle("overflow-hidden");
    }

    document.querySelectorAll(".js-header-menu-toggler").forEach((item) => {
        item.addEventListener("click", toggleMenu);
    });

    // document.querySelectorAll(".overflow-hidden").forEach((item) => {
    //     item.addEventListener("click", toggleMenu);
    // });
    // document.querySelector('.overflow-hidden').forEach((item) => {
    //     item.addEventListener("click", toggleMenu);
    // });

    


    // close the menu by clicking outside of it
    backdrop.addEventListener("click", toggleMenu);
    // window.addEventListener("click", toggleMenu);

    function collapse() {
        menu.querySelector(".active .js-sub-menu").removeAttribute("style");
        menu.querySelector(".active").classList.remove("active");
    }

    menu.addEventListener("click", (envent) => {
        const {target} = envent;
        
        if(target.classList.contains("js-toggle-sub-menu") && 
        window.innerWidth <= menuCollapseBrealpoit){
            // prevent default anchor click behavior
            envent.preventDefault();

            // if menu-item already expanded, collapse it and exit
            if (target.parentElement.classList.contains("active")) {
                collapse();
                return;
            }

            // if not already expaned... run below code

            // collapse the other expanded menu-item if exists
            if(menu.querySelector(".active")){
                collapse();
            }

            // expand new menu-item
            target.parentElement.classList.add("active");
            target.nextElementSibling.style.maxHeight = 
            target.nextElementSibling.scrollHeight + "px";
        }
    });

    // when resizing window
    window.addEventListener("resize", function () {
        if(this.innerWidth > menuCollapseBrealpoit && menu.classList.contains("open")){
            toggleMenu();
        }
        if (this.innerWidth > menuCollapseBrealpoit && menu.querySelector(".active")) {
            collapse();
        }
    })
}
headerMenu();


// style switcher
function styleSwitcherToggle(){
    const styleSwitcher = document.querySelector(".js-style-switcher"),
    styleSwitcherToggler = document.querySelector(".js-style-switcher-toggler");
    // console.log(1);

    styleSwitcherToggler.addEventListener("click", function(){
        console.log(styleSwitcher);
        styleSwitcher.classList.toggle("open");
        this.querySelector("i").classList.toggle("fa-times");
        this.querySelector("i").classList.toggle("fa-cog");
    });
}
styleSwitcherToggle();


// theme colors
function themeColors(){
    const colorStyle = document.querySelector(".js-color-style"),
    themeColorsContainer = document.querySelector(".js-theme-colors");

    themeColorsContainer.addEventListener("click", ({target}) => {
        if(target.classList.contains("js-theme-color-item")){
            localStorage.setItem("color", target.getAttribute("data-js-theme-color"));
            setColor();
        }
    });

    function  setColor(){
        let path = colorStyle.getAttribute("href").split("/");
        path = path.slice(0, path.length-1);
        colorStyle.setAttribute("href", path.join("/") + "/" + localStorage.getItem("color") + ".css");

        if(document.querySelector(".js-theme-color-item.active")){
            document.querySelector(".js-theme-color-item.active").classList.remove("active");
        }
        document.querySelector("[data-js-theme-color=" + localStorage.getItem("color") + "]").classList.add("active");
    }

    if(localStorage.getItem("color") !== null){
        setColor();
    }
    else{
        const defaultColor = colorStyle.getAttribute("href").split("/").pop().split(".").shift();
        document.querySelector("[data-js-theme-color=" + defaultColor + "]").classList.add("active");
        
    }

}
themeColors();


// theme light & dark mode
function themeLightDark(){
    const darkModeCheckbox = document.querySelector(".js-dark-mode");
    darkModeCheckbox.addEventListener("click", function(){
        if(this.checked){
            localStorage.setItem("theme-dark", "true");
        }
        else{
            localStorage.setItem("theme-dark", "false");
        }
        themeMode();
    });

    function themeMode(){
        if(localStorage.getItem("theme-dark") === "false"){
            document.body.classList.remove("t-dark");
        }
        else{
            document.body.classList.add("t-dark");
        }
    }

    if(localStorage.getItem("theme-dark") !== null){
        themeMode();
    }
    
    if(document.body.classList.contains("t-dark")){
        darkModeCheckbox.checked = true;
    }
    
}
themeLightDark();

// theme glass effect
function themeGlassEffect(){
    const glassEffectCheckbox = document.querySelector(".js-glass-effect"),
    glassStyle = document.querySelector(".js-glass-style");

    glassEffectCheckbox.addEventListener("click", function(){
        if(this.checked){
            localStorage.setItem("glass-effect", "true");
        }
        else{
            localStorage.setItem("glass-effect", "false");
        }
        glass();
    });

    function glass(){
        if(localStorage.getItem("glass-effect") === "true"){
            glassStyle.removeAttribute("disabled");
        }
        else{
            glassStyle.disabled = true;
        }
    }

    if(localStorage.getItem("glass-effect") !== null){
        glass();
    }
    
    if(!glassStyle.hasAttribute("disabled")){
        glassEffectCheckbox.checked = true;
    }
}
themeGlassEffect();

// Set active for exam status
$(document).ready(function() {
  $('input[type="radio"]').change(function() {
      var formId = $(this).closest('form').attr('id');
      $('#choice_' + formId).addClass('active');
  });
});


$(document).ready(function() {
  $("#finish").click(function() {
    var answersList = [];
    //Loop over all questoins
    $(".exam_page").each(function() {

      var questionId = $(this).attr("id");
      var answer = $("input[name='answer']:checked", $(this)).val();

      //if Answer isnt provided do not update the answersList
      if (answer !== undefined) {
        answersList.push({
          question: questionId,
          answer: answer
        });
      }
    });
    console.log(answersList);
  });
});
