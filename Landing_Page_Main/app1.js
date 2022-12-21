const observer = new IntersectionObserver((entries) => {
  entries.forEach((entry) => {
    if (entry.isIntersecting) {
      entry.target.classList.add("show1");
    } else {
      entry.target.classList.remove("show1");
    }
  });
});

const hiddenElements = document.querySelectorAll(".hidden1");

hiddenElements.forEach((element) => {
  observer.observe(element);
});

