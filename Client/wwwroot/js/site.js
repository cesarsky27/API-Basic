////// please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
////// for details on configuring this project to bundle and minify static web assets.

////// write your javascript code.

//////console.log("testing berhasil");

/*addeventlistener javascript*/
//var data = document.getelementbyid("judul");
//data.addeventlistener("click", function () {
//    data.innerhtml = "berubahhh";
//    data3.style.backgroundcolor = "green";
//});

//var data = getelementsbyclassname("button");
//data.addeventlistener("click", function () {
//    data.innerhtml = "test";
//})


//var data2 = document.getelementsbyclassname('list');

//for (var i = 0; i < data2.length; i++) {
//    data2[0].style.backgroundcolor = "lightgreen";
//}

////queryselector
//var data3 = document.queryselector("section#a p.p2");
//var data4 = document.queryselectorall(".list");
////jquery innerhtml
//var data5 = $(" .list").html("halo ini dirubah dengan jquery");
//var change = $(" .p1").html("testing");

////jquery function onclick, sama seperti addeventlistener
//$("h1#judul").on("click", function () {
//    $("section#a p.p2").html("berubahhhh")
//})

////$("h1#judul2").on("click", function () {
////    $("section#b ul li.list:nth-child(2)").html("berubah dong")
////})

////javascript innerhtml 
//var data6 = document.getelementbyid("judul2");
//data6.addeventlistener("click", function () {
//    data6.innerhtml = 'changed';
//    data3.style.backgroundcolor = "blue";
//})

//var data7 = document.getelementbyid('abc');
//data7.addeventlistener("click", function () {
//    data6.innerhtml = 'changed2';
//})

function myFunctionGajah() {
    document.getelementbyid("deskripsi").innerhtml = 'paus hitam putih';
    document.getelementbyid("mamalia-2").innerhtml = 'makanannya terdiri atas sedikitnya 50% sayur-sayuran, ditambah dengan dedaunan, ranting, akar, dan sedikit buah, benih dan bunga. karena gajah hanya mencerna 40% dari yang dimakannya, mereka harus mengonsumsi makanan dalam jumlah besar. gajah dewasa dapat mengonsumsi 300 hingga 600 pon (140-270 kg) makanan per hari ';
    document.getelementbyid('image-2').src= '/gambar/sayur.jpg';
}
function myFunctionPaus() {
    document.getelementbyid("mamalia-1").innerhtml = 'ikan paus menyukai makanan yang sebagian besar terdiri dari krill, kepiting merah, udang, dan ikan berkelompok.';
    document.getelementbyid('image-1').src = '/gambar/ikankecil.jpg';
}

function myFunctionKelelawar() {
    document.getelementbyid("mamalia-3").innerhtml = 'kelelawar jenis pemakan buah akan memakan buah dan membuang ampas berikut biji-biji buah tersebut, sehingga ia berperan memencarkan dan menyebarluaskan berbagai jenis tanamam berbuah ke daerah yang lebih luas. kotoran kelelawar juga dapat berfungsi sebagai pupuk organik untuk peningkatan pertumbuhan tanaman.';
    document.getelementbyid('image-3').src = '/gambar/buah.jpg';
}
function myFunctionPrimata() {
    document.getelementbyid('image-4').src = '/gambar/pisang.jpg';
    document.getelementbyid('mamalia-4').innerhtml = 'hewan primata ini memang menyukai buah-buahan, kacang-kacangan, biji-bijian, bunga hingga serangga. selain itu, mereka juga bisa memakan daging, kadal serta laba-laba. namun favorit makanan para primata adalah buah pisang';
}


$.ajax({
    url: "https://pokeapi.co/api/v2/pokemon/"
}).done((result) => {
    console.log(result.results);
    var textPokemon = "";
    $.each(result.results, function (key, val) {
        textPokemon += ` <tr>
            <td>${key+1}</td>
            <td>${val.name}</td>
            <td><button class="btn btn-primary" data-toggle="modal" data-target="#pokeModal" onclick = "getDataPokemon('${val.url}')">Detail</button></td>
             
        </tr>`;
    });
    console.log(textPokemon);
    $(" .tabelPoke").html(textPokemon);
}).fail((error) => {
    console.log(error);
});

function getDataPokemon(url) {
    $.ajax({
        url: url
    }).done((result) => {
        $("#tabelPoke").html(text);
        var img = result.sprites.other.dream_world.front_default
        var text = "";
        text = `       
        <img class="img-circle center-block d-block mx-auto " alt="100x100" src="${img}" />
<br>
<div class="text-lg-center">
<p class="pokeSkill">Abillities</p>
</div>
<div class="text-center">`
        for (let i = 0; i < result.abilities.length; i++) {

            var ability1 = result.abilities[i].ability.name;
            text += `
              
             <label id="abilities" class="badge">
             <span class="text-capitalize pb-2 align-middle">${ability1}</span>
            </label>`
        }
        var type1 = result.types[0].type.name;
        var weight = result.weight;
        var height = result.height;
        var base_experience = result.base_experience;

        text += `
              <p class="pokeType">Pokemon Type</p>
<label id="type" class="badge">
 <span class="text-capitalize pb-2 align-middle">${type1}</span>
</label>
</div>

<div class="text-lg-center pokeDetail">
     <label>
 <span class="text-uppercase pb-2 align-middle" ><p class= "pokeName">${result.name}</p></span>
</label>
</div>
<div class="">
        <table class="table table-borderless">
            <tr>
                <td> <h6>Weight : </h6></td>
                <td><h6>${weight}</h6></>
            </tr>
            <tr>
                <td><h6>Height : </h6></td>
                <td><h6>${height}</h6></>
            </tr>
            <tr>
                <td> <h6>Base Experience : </h6></td>
                <td><h6>${base_experience}</h6></>
            </tr>
       </table>
</div> `

        $('.modal-body').html(text).css('background-color', 'white');
    }).fail((error) => {
        console.log(error);
    });
}
///*tugas-1 pemahaman array of objects*/
//const animals = [
//    { name: 'bimo', species: 'cat', kelas: { name: "mamalia" } },
//    { name: 'budi', species: 'cat', kelas: { name: "mamalia" } },
//    { name: 'nemo', species: 'snail', kelas: { name: "invertebrata" } },
//    { name: 'dori', species: 'cat', kelas: { name: "mamalia" } },
//    { name: 'simba', species: 'snail', kelas: { name: "invertebrata" } }
//]
//let onlycat = [];
//for (var i = 0; i < animals.length; i++) {
//    if (animals[i].species === "cat") {
//        onlycat.push(animals[i]);
//    }
//    else {
//        animals[i].kelas.name = 'non-mamalia';
//    }
//}
//console.log(animals);
//console.log(onlyCat);
