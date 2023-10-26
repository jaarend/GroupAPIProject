const uri_attack = "https://localhost:7124/api/attack";
const uri_token = "https://localhost:7124/api/token";

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
const buttonAttackUpdate = document.querySelector("#put-attack");
function changeAttackItem() {
  const idOfUpdatedItem = document.getElementById("attack-id");
  const changeNameTextbox = document.getElementById("change-name");
  const changeDescriptionTextbox =
    document.getElementById("change-description");
  const changeTypeTextbox = document.getElementById("change-type");
  const changeHitValueTextbox = document.getElementById("change-hitvalue");
  const changeAPCostTextbox = document.getElementById("change-apcost");
  const type = parseInt(changeTypeTextbox.value, 10);
  const hitvalue = parseInt(changeHitValueTextbox.value, 10);
  const apcost = parseInt(changeAPCostTextbox.value, 10);
  const attackid = parseInt(idOfUpdatedItem.value, 10);
  const item = {
    id: attackid,
    name: changeNameTextbox.value.trim(),
    description: changeDescriptionTextbox.value.trim(),
    type: type,
    hitvalue: hitvalue,
    apcost: apcost,
  };
  fetch(uri_attack, {
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
      // getItems(); this needs to be built out
      //idOfUpdatedItem.value = "";
      changeNameTextbox.value = "";
      changeDescriptionTextbox.value = "";
      changeTypeTextbox.value = "";
      changeHitValueTextbox.value = "";
      changeAPCostTextbox.value = "";
    })
    .catch((error) => console.error("Unable to add item.", error));
}
buttonAttackUpdate.addEventListener("click", function (e) {
  e.preventDefault();
  changeAttackItem();
});


const buttonGetAllAttacks = document.querySelector("#get-attack");
let attacksList = document.querySelector("ul");
function getAllAttacks() {
  fetch(uri_attack, {
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
          a.id +
          ", " +
          a.name +
          ", " +
          a.description +
          ", " +
          a.type +
          ", " +
          a.hitValue +
          ", " +
          a.apCost +
          ", " +
          a.dateCreated;
        attacksList.appendChild(listItem);
      }
      // getItems();this needs to be built out
    })
    .catch((error) => console.error("Unable to add item.", error));
}
buttonAttackUpdate.addEventListener("click", function (e) {
  e.preventDefault();
  getAllAttacks();
});
