const uri_gear = "https://localhost:7124/api/gear";

const buttonGear = document.querySelector("#post-gear");
const buttonLogInUser = document.querySelector("#login-user");

function createGear() {
    const addNameTextbox = document.getElementById("add-name");
    const addDescriptionTextbox = document.getElementById("add-description");
    const addTypeTextbox = document.getElementById("add-type");
    const addValueTextbox = document.getElementById("add-value");
    
    const type = parseInt(addTypeTextbox.value, 10);
    const value = parseInt(addValueTextbox.value, 10);
    
    const item = {
        name: addNameTextbox.value.trim(),
        description: addDescriptionTextbox.value.trim(),
        type: type,
        value: value,
    };

    fetch(uri_gear, {
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

        addNameTextbox.value = "";
        addDescriptionTextbox.value = "";
        addTypeTextbox.value = "";
        addValueTextbox.value = "";
    })
    .catch((error) => console.error("Unable to add item.", error));
}

buttonGear.addEventListener("click", function (e){
    e.preventDefault();
    createGear();
});








