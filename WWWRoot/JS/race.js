const uri_race = "https://localhost:7124/api/race";

const buttonNewRace = document.querySelector("#post-race");
const buttonGetAllRaces = document.querySelector("#get-race");
const buttonGetRaceById = document.querySelector("#get-race-button");
const buttonUpdateRace = document.querySelector("#update-race");
const buttonDeleteRace = document.querySelector("#delete-race");


function addNewRace() {
  let nameInput = document.getElementById("add-race-name");
  let descriptionInput = document.getElementById("add-race-description");
  let strengthInput = document.getElementById("add-race-strengthModifier");
  let constitutionInput = document.getElementById("add-race-constitutionModifier");
  let intelligenceInput = document.getElementById("add-race-intelligenceModifier");

  let strength = parseInt(strengthInput.value);
  let constitution = parseInt(constitutionInput.value);
  let intelligence = parseInt(intelligenceInput.value);

  let item = {
    name: nameInput.value.trim(),
    description: descriptionInput.value.trim(),
    strengthModifier: strength,
    constitutionModifier: constitution,
    intelligenceModifier: intelligence,
  };

  fetch(uri_race, {
    method: "POST",
    mode: "cors",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(item),
  })
  .then((response) => response.json())
  .then((data) =>  {
    console.log("Received data from the server.", data);
  })
  .catch((error) => console.error("Unable to add item.", error));
}

buttonNewRace.addEventListener("click", function (e) {
  e.preventDefault();
  addNewRace();
});


function getAllRaces() {
  const raceList = document.getElementById("all-race-list");
  fetch(uri_race, {
    method: "GET",
    mode: "cors",
    headers: { "Content-Type": "application/json" },
  })
    .then((response) => response.json())
    .then((data) => {
      console.log("Received data from the server.", data);
      for (const a of data) {
        let listItem = document.createElement("li");
        listItem.innerText =
          "Id: " + a.id + "\n Name: " + a.name + "\n Description: " +
          a.description + "\n Strength Modifier: " + a.strengthModifier +
          "\n Constitution Modifier " + a.constitutionModifier +
          "\n Intelligence Modifier " + a.intelligenceModifier +
          "\n Date Created: " + a.dateCreated;
        raceList.appendChild(listItem);
      }
      // getItems();this needs to be built out
    })
    .catch((error) => console.error("Unable to add item.", error));
}

buttonGetAllRaces.addEventListener("click", function (e) {
  e.preventDefault();
  getAllRaces();
});


function GetRaceById() {
  let idInput = document.getElementById("get-race-id");
  let raceDisplay = document.getElementById("race-id-display");

  let identity = parseInt(idInput.value);

  fetch((uri_race + "/" + identity), {
    method: "GET",
    mode: "cors",
  })
  .then((response) => response.json())
  .then((a) => {
    console.log("Received data from the server.", a);
      let listItem = document.createElement("li");
      listItem.innerText =
        "Id: " + a.id + "\n Name: " + a.name + "\n Description: " +
        a.description + "\n Strength Modifier: " + a.strengthModifier +
        "\n Constitution Modifier " + a.constitutionModifier +
        "\n Intelligence Modifier " + a.intelligenceModifier +
        "\n Date Created: " + a.dateCreated;
      raceDisplay.appendChild(listItem);
  })
  .catch((error) => console.error("Unable to add items.", error));
}

buttonGetRaceById.addEventListener("click", function (e) {
  e.preventDefault();
  GetRaceById();
})


function updateRace() {
  let idInput = document.getElementById("update-race-id");
  let nameInput = document.getElementById("update-race-name");
  let descriptionInput = document.getElementById("update-race-description");
  let strengthInput = document.getElementById("update-race-strengthModifier");
  let constitutionInput = document.getElementById("update-race-constitutionModifier");
  let intelligenceInput = document.getElementById("update-race-intelligenceModifier");

  let identity = parseInt(idInput.value);
  let strength = parseInt(strengthInput.value);
  let constitution = parseInt(constitutionInput.value);
  let intelligence = parseInt(intelligenceInput.value);

  let item = {
    id: identity,
    name: nameInput.value.trim(),
    description: descriptionInput.value.trim(),
    strengthModifier: strength,
    constitutionModifier: constitution,
    intelligenceModifier: intelligence,
  };

  fetch(uri_race, {
    method: "PUT",
    mode: "cors",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(item),
  })
  .then((response) => response.json())
  .then((data) => {
    console.log("Received data from the server.", data);
  })
  .catch((error) => console.error("Unable to add items.", error));
}

buttonUpdateRace.addEventListener("click", function (e) {
  e.preventDefault();
  updateRace();
});


function deleteRace() {
  let idInput = document.getElementById("delete-race-id");

  let identity = parseInt(idInput.value);

  let item = {
    id: identity,
  };

  fetch(uri_race, {
    method: "DELETE",
    mode: "cors",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(item),
  })
  .then((response) => response.json())
  .then((data) => {
    console.log("Received data from the server.", data);
  })
  .catch((error) => console.error("Unable to delete item.", error));
}

buttonDeleteRace.addEventListener("click", function (e) {
  e.preventDefault();
  deleteRace();
});