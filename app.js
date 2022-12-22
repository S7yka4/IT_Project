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









const observerup = new IntersectionObserver((entriesup) => {
  entriesup.forEach((entryup) => {
    if (entryup.isIntersecting) {
      entryup.target.classList.add("showup");
    } else {
      entryup.target.classList.remove("showup");
    }
  });
});

const hiddenElementsup = document.querySelectorAll(".hiddenup");

hiddenElementsup.forEach((element) => {
  observerup.observe(element);
});








const observer6 = new IntersectionObserver((entries6) => {
  entries6.forEach((entry6) => {
    if (entry6.isIntersecting) {
      entry6.target.classList.add("show6");
    } else {
      entry6.target.classList.remove("show6");
    }
  });
});

const hiddenElements6 = document.querySelectorAll(".hidden6");

hiddenElements6.forEach((element6) => {
  observer6.observe(element);
});