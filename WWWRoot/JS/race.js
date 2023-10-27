const uri_race = "https://localhost:7124/api/race";


const buttonGetAllClasses = document.querySelector("#get-race");
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
        raceList.appendChild(listItem);
      }
      // getItems();this needs to be built out
    })
    .catch((error) => console.error("Unable to add item.", error));
}
buttonGetAllClasses.addEventListener("click", function (e) {
  e.preventDefault();
  getAllRaces();
});
