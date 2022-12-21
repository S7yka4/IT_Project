function scrollTo(element) {
  window.scroll({
    left: 0, 
    top: element.offsetTop, 
    behavior: 'smooth'
  })
}

var button = document.querySelector('.item');
var footer = document.querySelector('.down');

button.addEventListener('click', () => {
  scrollTo(footer);
})