function addToList() {

    const customer1 = {
        firstName:"jack",
        lastName:"asd",
        age:"21"
    } 
    console.log(customer1.firstName);
    console.log(customer1.lastName);
    console.log(customer1.age);

    const firstCust = document.getElementById("firstNameInput").value;
    const lastCust = document.getElementById("lastNameInput").value;
    const ageCust = JSON.stringify(document.getElementById("ageInput").value);
    console.log(firstCust);
    console.log(lastCust);
    console.log(ageCust);

    const customer = {
        firstName : firstCust,
        lastName : lastCust,
        age : ageCust
    };
    console.log(customer.firstName);
    console.log(customer.lastName);
    console.log(customer.age);
    const listCustomer=[];
    listCustomer.push(customer);
    localStorage.setItem("listCustomer",JSON.stringify(listCustomer));
    console.log(listCustomer);



    const table = document.getElementById("inputGrid");
    const row = document.createElement("tr");
    const firstNameColumn = document.createElement("th")
    firstNameColumn.innerHTML = document.getElementById("firstNameInput").value;
    row.appendChild(firstNameColumn);
    const lastNameColumn = document.createElement("th")
    lastNameColumn.innerHTML = document.getElementById("lastNameInput").value;
    row.appendChild(lastNameColumn);
    const ageColumn = document.createElement("th")
    ageColumn.innerHTML = document.getElementById("ageInput").value;
    row.appendChild(ageColumn);
    table.appendChild(row);
 

}


function ageList(){

    var selectMenu = document.getElementById("ageInput");

    for (let counter = 0; counter < 100; counter++ ){

        const age = document.createElement("option");
        age.setAttribute("value", counter+1);
        age.innerHTML= counter+1;
        selectMenu.appendChild(age);


    }
}


function showList(){

    const table = document.getElementById("inputGrid");
    console.log(localStorage);
    for (var entry in localStorage){
        
        const row = document.createElement("tr");
        const firstNameColumn = document.createElement("th")
        firstNameColumn.innerHTML = entry.firstName;
        row.appendChild(firstNameColumn);
        const lastNameColumn = document.createElement("th")
        lastNameColumn.innerHTML = entry.lastName;
        row.appendChild(lastNameColumn);
        const ageColumn = document.createElement("th")
        ageColumn.innerHTML = entry.age;
        row.appendChild(ageColumn);
        table.appendChild(row);
    }
}