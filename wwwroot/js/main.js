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
