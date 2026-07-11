let medicines=[];async function loadMedicines(){const r=await fetch('/api/medicines');medicines = await r.json();render(medicines);populateSaleDropdown();}function render(data){const t=document.querySelector('#medicineTable tbody');t.innerHTML='';data.forEach(m=>{let c='';const d=(new Date(m.expiryDate)-new Date())/86400000;if(d<30)c='red';else if(m.quantity<10)c='yellow';t.innerHTML+=`<tr class="${c}"><td>${m.fullName}</td><td>${m.expiryDate.substring(0,10)}</td><td>${m.quantity}</td><td>${Number(m.price).toFixed(2)}</td><td>${m.brand}</td></tr>`;});}document.addEventListener('DOMContentLoaded',()=>{document.getElementById('searchBox').addEventListener('keyup',e=>{const v=e.target.value.toLowerCase();render(medicines.filter(x=>x.fullName.toLowerCase().includes(v)));});loadMedicines();});
 async function addMedicine(){const m = {

    FullName: document.getElementById("name").value,

    Notes: document.getElementById("notes").value,

    ExpiryDate: document.getElementById("expiry").value,

    Quantity: parseInt(document.getElementById("qty").value),

    Price: parseFloat(document.getElementById("price").value),

    Brand: document.getElementById("brand").value

};await fetch('/api/medicines',{method:'POST',headers:{'Content-Type':'application/json'},body:JSON.stringify(m)});loadMedicines();loadSales();}
function populateSaleDropdown() {


    const ddl =

        document.getElementById("saleMedicine");


    ddl.innerHTML = "";


    medicines.forEach(m => {


        ddl.innerHTML +=

            `<option value="${m.id}">

                ${m.fullName}

            </option>`;

    });

}
async function loadSales() {


    const response =

        await fetch('/api/sales');


    const sales =

        await response.json();


    const tbody =

        document.querySelector(

            '#salesTable tbody');


    tbody.innerHTML = '';


    sales.forEach(s => {


        tbody.innerHTML += `

            <tr>

                <td>${s.id}</td>

                <td>${s.medicineId}</td>

                <td>${s.quantitySold}</td>

                <td>${s.soldDate}</td>

            </tr>

        `;

    });

}
async function recordSale() {


    const sale = {


        medicineId:

            parseInt(

                document

                .getElementById('saleMedicine')

                .value),


        quantitySold:

            parseInt(

                document

                .getElementById('saleQuantity')

                .value)

    };


    await fetch('/api/sales', {


        method: 'POST',


        headers: {

            'Content-Type': 'application/json'

        },


        body: JSON.stringify(sale)

    });


    loadMedicines();

    loadSales();

}
 