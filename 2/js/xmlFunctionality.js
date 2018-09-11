function loadXMLDoc(dname) {
    try {
        var xmlhttp = new XMLHttpRequest();
        xmlhttp.onreadystatechange = function () {
            if (this.readyState == 4 && this.status == 200) {
                myFunction(this);
            }
        };
        xmlhttp.open("GET", dname, true);
        xmlhttp.send();
    }
    catch (err) {
        alert(err);
    }
}

function myFunction(xml) {
    try {
        var i, j;
        var xmlDoc = xml.responseXML;
        var table = "<table><tr><th>Artist</th><th>Title</th></tr>";
        var ulValue = "<a href=\"#\"><h4 class=\"sec - head\">project Maintenance</h4></a><p>taque quas officiis iure</p><ul>";
        var x = xmlDoc.getElementsByTagName("JOB");
        var skill = "";
        var title = "";

        for (i = 0; i < x.length; i++) {
            title = x[i].getElementsByTagName("Title")[0].childNodes[0].nodeValue;
            skill = x[i].getElementsByTagName("Skills")[0].childNodes;
            table += "<tr><td>" +
                title +
                "</td></tr>";
            for (j = 0; j < skill.length; j++) {
                if (skill[j].nodeName == "Skill") {
                    ulValue += "<li>< i class=\"fa fa-check\" ></i > " + skill[j].textContent + " <//li >";
                }
            }
        }

        ulValue += "</ul><ul></ul>";

        table += "</table>";
        document.getElementById("career").style.display = "none";
        document.getElementById("jobPosting").style.display = "";

        document.getElementById("jobTitle").innerHTML = title;
        document.getElementById("jobUL").innerHTML = ulValue;
        document.getElementById("demoNew").innerHTML = table;
        //document.getElementById("modal01").style.display = "block";
    }
    catch (err) {
        alert(err);
    }
}

function searchXML(element) {
    try {
        loadXMLDoc(element.name)
    }
    catch (err) {
        alert(err.message);
    }
}

$(document).ready(function () {
    $('#example').DataTable({
        initComplete: function () {
            this.api().columns().every(function () {
                var column = this;
                var select = $('<select><option value=""></option></select>')
                    .appendTo($(column.footer()).empty())
                    .on('change', function () {
                        var val = $.fn.dataTable.util.escapeRegex(
                            $(this).val()
                        );

                        column
                            .search(val ? '^' + val + '$' : '', true, false)
                            .draw();
                    });

                column.data().unique().sort().each(function (d, j) {
                    select.append('<option value="' + d + '">' + d + '</option>')
                });
            });
        }
    });
});