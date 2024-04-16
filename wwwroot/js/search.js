/*=============== SEARCH ===============*/

function Search() {

    const search = document.getElementById('search'),
        searchBtn = document.getElementById('btn-search'),
        searchClose = document.getElementById('btn-close')

    /* Search show */
    searchBtn.addEventListener('click', () => {
        search.classList.add('show-search')
    })

    /* Search hidden */
    searchClose.addEventListener('click', () => {
        search.classList.remove('show-search')
    })
}
Search();