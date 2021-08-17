window.onscroll = function() {myFunction()};

function myFunction() {
  //alert(document.getElementById("topHeader"));
  var header = document.getElementById("midHeader");
  var sticky = header.offsetTop;

  if (window.pageYOffset > sticky) {
    header.classList.add("sticky");
    //alert('1');
  } else {
    header.classList.remove("sticky");
    //alert('2');
  }
}