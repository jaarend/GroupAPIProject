const uri_user = "https://localhost:7124/api/user";
const uri_token = "https://localhost:7124/api/token";

const buttonUser = document.querySelector("#post-user");
const buttonLogInUser = document.querySelector("#login-user");


function addNewUser() {
  const addFirstNameTextbox = document.getElementById("add-firstname");
  const addLastNameTextbox = document.getElementById("add-lastname");
  const addEmailTextbox = document.getElementById("add-email");
  const addUserNameTextbox = document.getElementById("add-username");
  const addUserRoleTextbox = document.getElementById("add-userrole");
  const addPasswordTextbox = document.getElementById("add-password");
  const addConfirmPasswordTextbox = document.getElementById("confirm-password");

  const userRole = parseInt(addUserRoleTextbox.value, 10); //might want to hide this or make it automatic

  const item = {
    firstname: addFirstNameTextbox.value.trim(),
    lastname: addLastNameTextbox.value.trim(),
    email: addEmailTextbox.value.trim(),
    username: addUserNameTextbox.value.trim(),
    userrole: userRole,
    password: addPasswordTextbox.value.trim(),
    confirmpassword: addConfirmPasswordTextbox.value.trim(),
  };

  fetch(uri_user, {
    method: "POST",
    mode: "cors",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(item),
  })
    .then((response) => response.json())
    .then((data) => {
      console.log("Received data from the server.", data);

      // getItems(); this needs to be built out
      addFirstNameTextbox.value = "";
      addLastNameTextbox.value = "";
      addEmailTextbox.value = "";
      addUserNameTextbox.value = "";
      addUserRoleTextbox.value = "";
      addPasswordTextbox.value = "";
      addConfirmPasswordTextbox.value = "";
    })
    .catch((error) => console.error("Unable to add item.", error));

  buttonUser.addEventListener("click", function (e) {
    e.preventDefault();
    addNewUser();
  });
}



