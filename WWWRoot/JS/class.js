const uri_class = "https://localhost:7124/api/class";
const uri_token = "https://localhost:7124/api/token";

const buttonClass = document.querySelector("#post-class");

function addClassItem() {
  const addNameTextbox = document.getElementById("add-name");
  const addDescriptionTextbox = document.getElementById("add-description");
  const addAttackSlot_1Textbox = document.getElementById("add-attackslot_1");
  const addAttackSlot_2Textbox = document.getElementById("add-attackslot_2");
  const addWeaponIdTextbox = document.getElementById("add-weaponid");
  const addArmorIdTextbox = document.getElementById("add-armorid");

  const AttackSlot_1 = parseInt(addAttackSlot_1Textbox.value, 10);
  const AttackSlot_2 = parseInt(addAttackSlot_2Textbox.value, 10);
  const WeaponId = parseInt(addWeaponIdTextbox.value, 10);
  const ArmorId = parseInt(addArmorIdTextbox.value, 10);

  const item = {
    name: addNameTextbox.value.trim(),
    description: addDescriptionTextbox.value.trim(),
    attackslot_1: AttackSlot_1,
    attackslot_2: AttackSlot_2,
    weaponid: WeaponId,
    armorid: ArmorId,
  };

  fetch(uri_class, {
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
      addAttackSlot_1Textbox.value = "";
      addAttackSlot_2Textbox.value = "";
      addWeaponIdTextbox.value = "";
      addArmorIdTextbox.value = "";
    })
    .catch((error) => console.error("Unable to add item.", error));
}

buttonClass.addEventListener("click", function (e) {
  e.preventDefault();
  addClassItem();
});

const buttonClassUpdate = document.querySelector("#put-class");
function changeClassItem() {
  const idOfUpdatedItem = document.getElementById("class-id");
  const ChangeNameTextbox = document.getElementById("change-name");
  const ChangeDescriptionTextbox = document.getElementById("change-description");
  const ChangeAttackSlot_1Textbox = document.getElementById("change-attackslot_1");
  const ChangeAttackSlot_2Textbox = document.getElementById("change-attackslot_2");
  const ChangeWeaponIdTextbox = document.getElementById("change-weaponid");
  const ChangeArmorIdTextbox = document.getElementById("change-armorid");
  const AttackSlot_1 = parseInt(ChangeAttackSlot_1Textbox.value, 1000);
  const AttackSlot_2 = parseInt(ChangeAttackSlot_2Textbox.value, 1000);
  const WeaponId = parseInt(ChangeWeaponIdTextbox.value, 1000);
  const ArmorId = parseInt(ChangeArmorIdTextbox.value, 1000);
  const classid = parseInt(idOfUpdatedItem.value, 1000);
  const item = {
    id: classid,
    name: ChangeNameTextbox.value.trim(),
    description: ChangeDescriptionTextbox.value.trim(),
    attackslot_1: AttackSlot_1,
    attackslot_2: AttackSlot_2,
    weaponid: WeaponId,
    armorid: ArmorId,
  };
  fetch(uri_class, {
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
      ChangeNameTextbox.value = "";
      ChangeDescriptionTextbox.value = "";
      ChangeAttackSlot_1Textbox.value = "";
      ChangeAttackSlot_2Textbox.value = "";
      ChangeWeaponIdTextbox.value = "";
      ChangeArmorIdTextbox.value = "";
    })
    .catch((error) => console.error("Unable to add item.", error));
}
buttonClassUpdate.addEventListener("click", function (e) {
  e.preventDefault();
  changeClassItem();
});

const buttonGetAllClasses = document.querySelector("#get-class");
let classesList = document.querySelector("ul");
function getAllClasses() {
  fetch(uri_class, {
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
          a.attackSlot_1 +
          ", " +
          a.attackSlot_2 +
          ", " +
          a.weaponId +
          ", " +
          a.armorId +
          ", " +
          a.dateCreated;
        classesList.appendChild(listItem);
      }
      // getItems();this needs to be built out
    })
    .catch((error) => console.error("Unable to add item.", error));
}
buttonClassUpdate.addEventListener("click", function (e) {
  e.preventDefault();
  getAllClasses();
});
