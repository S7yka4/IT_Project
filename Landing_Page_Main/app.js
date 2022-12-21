const observer = new IntersectionObserver((entries) => {
  entries.forEach((entry) => {
    if (entry.isIntersecting) {
      entry.target.classList.add("show");
    } else {
      entry.target.classList.remove("show");
    }
  });
});

const hiddenElements = document.querySelectorAll(".hidden");

hiddenElements.forEach((element) => {
  observer.observe(element);
});


const observer1 = new IntersectionObserver((entries1) => {
  entries1.forEach((entry1) => {
    if (entry1.isIntersecting) {
      entry1.target.classList.add("show1");
    } else {
      entry1.target.classList.remove("show1");
    }
  });
});

const hiddenElements1 = document.querySelectorAll(".hidden1");

hiddenElements1.forEach((element) => {
  observer1.observe(element);
});


const observer2 = new IntersectionObserver((entries2) => {
  entries2.forEach((entry2) => {
    if (entry2.isIntersecting) {
      entry2.target.classList.add("show2");
    } 
  });
});

const hiddenElements2 = document.querySelectorAll(".hidden2");

hiddenElements2.forEach((element) => {
  observer2.observe(element);
});

const observer3 = new IntersectionObserver((entries3) => {
  entries3.forEach((entry3) => {
    if (entry3.isIntersecting) {
      entry3.target.classList.add("show3");
    } 
    else {
      entry3.target.classList.remove("show3");
    }
  });
});

const hiddenElements3 = document.querySelectorAll(".hidden3");

hiddenElements3.forEach((element) => {
  observer3.observe(element);
});
