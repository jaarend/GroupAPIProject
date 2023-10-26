const uri_user = "https://localhost:7124/api/user";
const uri_character = "https://localhost:7124/api/character";
const uri_attack = "https://localhost:7124/api/attack";
const uri_token = "https://localhost:7124/api/token";

const buttonUser = document.querySelector("#post-user");
const buttonLogInUser = document.querySelector("#login-user");
const buttonNewCharacter = document.querySelector("#post-character");

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

let token = "";

function logInUser() {
  const loginUserNameTextbox = document.getElementById("login-username");
  const loginPasswordTextbox = document.getElementById("login-password");

  const loginItem = {
    username: loginUserNameTextbox.value.trim(),
    password: loginPasswordTextbox.value.trim(),
  };

  fetch(uri_token, {
    method: "POST",
    mode: "cors",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(loginItem),
  })
  .then((response) => response.json())
  .then((data) => {
    // Handle the token response, save it as a constant
    if (data.token) {
      token = data.token; //stores the token

    }
  })
  .catch((error) => console.error("Unable to get token.", error));
}

buttonLogInUser.addEventListener("click", function (e) {
  e.preventDefault();
  logInUser();
});


// function getCookie(name) {
//   const cookieValue = document.cookie
//     .split("; ")
//     .find((row) => row.startsWith(`${name}=`));
//     console.log("JWT value: ", cookieValue);
    
    
//     if (cookieValue) {
//       const value = cookieValue.split("=")[1];
//       const decodedValue = decodeURIComponent(value); // Decode the cookie value if necessary
//       return decodedValue;
//     }
  
//   return null;
// }

function addNewCharacter(token) {
  // const jwtToken = getCookie("jwtToken");

  const addCharacterNameTextbox = document.getElementById("add-character-name");
  const addCharacterDescriptionTextbox = document.getElementById(
    "add-character-description"
  );
  const addCharacterTypeTextbox = document.getElementById("add-character-type");
  const addCharacterRaceTextbox = document.getElementById(
    "add-character-raceId"
  );
  const addCharacterClassTextbox = document.getElementById(
    "add-character-classId"
  );

  const Type = parseInt(addCharacterTypeTextbox.value, 10);
  const RaceId = parseInt(addCharacterRaceTextbox.value, 10);
  const ClassId = parseInt(addCharacterClassTextbox.value, 10);

  const Armor = 0;
  const Strength = 0;
  const Constitution = 0;
  const Intelligence = 0;

  const item = {
    name: addCharacterNameTextbox.value.trim(),
    description: addCharacterDescriptionTextbox.value.trim(),
    type: Type,
    raceId: RaceId,
    classId: ClassId,
    armor: Armor,
    strength: Strength,
    constitution: Constitution,
    intelligence: Intelligence,
  };

  fetch(uri_character, {
    method: "POST",
    mode: "cors",
    headers: {
      "Content-Type": "application/json",
      "Authorization": `Bearer ${token}`,
    },
    body: JSON.stringify(item),
  })
    .then((response) => response.json())
    .then((data) => {
      console.log("Received data from the server.", data);
    })
    .catch((error) => console.error("Unable to add item.", error));
}

buttonNewCharacter.addEventListener("click", function (e) {
  e.preventDefault();
  addNewCharacter(token);
});

const buttonAttack = document.querySelector("#post-attack");

function addAttackItem() {
  const addNameTextbox = document.getElementById("add-name");
  const addDescriptionTextbox = document.getElementById("add-description");
  const addTypeTextbox = document.getElementById("add-type");
  const addHitValueTextbox = document.getElementById("add-hitvalue");
  const addAPCostTextbox = document.getElementById("add-apcost");

  const type = parseInt(addTypeTextbox.value, 10);
  const hitvalue = parseInt(addHitValueTextbox.value, 10);
  const apcost = parseInt(addAPCostTextbox.value, 10);

  const item = {
    name: addNameTextbox.value.trim(),
    description: addDescriptionTextbox.value.trim(),
    type: type,
    hitvalue: hitvalue,
    apcost: apcost,
  };

  fetch(uri_attack, {
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
      addNameTextbox.value = "";
      addDescriptionTextbox.value = "";
      addTypeTextbox.value = "";
      addHitValueTextbox.value = "";
      addAPCostTextbox.value = "";
    })
    .catch((error) => console.error("Unable to add item.", error));
}

buttonAttack.addEventListener("click", function (e) {
  e.preventDefault();
  addAttackItem();
});
