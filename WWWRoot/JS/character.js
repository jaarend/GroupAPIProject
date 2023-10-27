const uri_user = "https://localhost:7124/api/user";
const uri_character = "https://localhost:7124/api/character";
const uri_attack = "https://localhost:7124/api/attack";
const uri_token = "https://localhost:7124/api/token";

const buttonUser = document.querySelector("#post-user");
const buttonLogInUser = document.querySelector("#login-user");
const buttonNewCharacter = document.querySelector("#post-character");
const buttonUpdateCharacter = document.querySelector("#update-character");
const buttonDeleteCharacter = document.querySelector("#delete-character");
const buttonGetAllCharactersLoggedIn = document.querySelector("#getall-character-loggedin");

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
      Authorization: `Bearer ${token}`,
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

function getAllCharactersLoggedIn(token) {
  const characterList = document.getElementById("all-character-list");
  fetch(uri_character, {
    method: "GET",
    mode: "cors",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify(),
  })
    .then((response) => response.json())
    .then((data) => {
      console.log("Received data from the server.", data);
      characterList.innerHTML = "";

      // Iterate through the data and create li elements
      for (const c of data)
      {
        const li = document.createElement("li");
        li.innerHTML = c.name + " " + c.description + " " +  c.armor;
        characterList.appendChild(li);
      }
    })
    .catch((error) => console.error("Unable to delete item.", error));
}
buttonGetAllCharactersLoggedIn.addEventListener("click", function (e) {
  e.preventDefault();
  getAllCharactersLoggedIn(token);
});

function updateCharacter(token) {
  const updateCharacterIdTextbox = document.getElementById("update-character-id");
  const updateCharacterNameTextbox = document.getElementById("update-character-name");
  const updateCharacterDescriptionTextbox = document.getElementById("update-character-description");
  const updateCharacterTypeTextbox = document.getElementById("update-character-type");
  
  const Id = parseInt(updateCharacterIdTextbox.value, 10);
  const Type = parseInt(updateCharacterTypeTextbox.value, 10);

  const item = {
    id: updateCharacterIdTextbox.value.trim(),
    name: updateCharacterNameTextbox.value.trim(),
    description: updateCharacterDescriptionTextbox.value.trim(),
    type: Type,
  };

  fetch(uri_character, {
    method: "PUT",
    mode: "cors",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify(item),
  })
    .then((response) => response.json())
    .then((data) => {
      console.log("Received data from the server.", data);
    })
    .catch((error) => console.error("Unable to add item.", error));

}

buttonUpdateCharacter.addEventListener("click", function (e) {
  e.preventDefault();
  updateCharacter(token);
});

function deleteCharacter(token) {
  const updateCharacterIdTextbox = document.getElementById("delete-character-id");

  
  const Id = parseInt(updateCharacterIdTextbox.value, 10);

  const item = {
    id: Id,
  };

  fetch(uri_character, {
    method: "DELETE",
    mode: "cors",
    headers: {
      "Content-Type": "application/json",
      Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify(item),
  })
    .then((response) => response.json())
    .then((data) => {
      console.log("Received data from the server.", data);
    })
    .catch((error) => console.error("Unable to delete item.", error));

}

buttonDeleteCharacter.addEventListener("click", function (e) {
  e.preventDefault();
  deleteCharacter(token);
});
