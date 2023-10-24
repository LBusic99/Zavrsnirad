$(document).ready(function () {
    var addButton = $("#addResidentButton");
    var updateButton = $("<button>").text("Izmjeni podatke štićenika").addClass("update-button");
    var cancelButton = $("<button>").text("Poništi izmjenu").addClass("cancel-button");
    function displayResidents() {
        $.ajax({
            type: "GET",
            url: "/api/residents",
            success: function (data) {
                $("#residentList").empty();
                data.forEach(function (resident) {
                    var listItem = $("<li>");
                    var residentInfo = $("<div>");
                    var formattedAdmissionDate = new Date(resident.admissionDate).toLocaleDateString('en-GB', { year: 'numeric', month: '2-digit', day: '2-digit' });
                    residentInfo.addClass("resident-info");
                    residentInfo.append($("<strong>").text("ID Štićenika: "));
                    residentInfo.append($("<span>").text(resident.residentID));
                    residentInfo.append($("<br>"));
                    residentInfo.append($("<strong>").text("Ime i prezime: "));
                    residentInfo.append($("<span>").text(resident.firstName + " " + resident.lastName));
                    residentInfo.append($("<br>"));
                    residentInfo.append($("<strong>").text("Broj sobe: "));
                    residentInfo.append($("<span>").text(resident.roomNumber));
                    residentInfo.append($("<br>"));
                    residentInfo.append($("<strong>").text("Datum upisa (DD/MM/YYYY): "));
                    residentInfo.append($("<span>").text(formattedAdmissionDate));
                    residentInfo.append($("<br>"));
                    residentInfo.append($("<strong>").text("Medicinsko stanje: "));
                    residentInfo.append($("<span>").text(resident.medicalCondition));
                    var editButton = $("<button>").text("Uredi podatke").addClass("edit-button");
                    var removeButton = $("<button>").text("Obriši štićenika").addClass("remove-button");
                    residentInfo.append(editButton);
                    residentInfo.append(removeButton);
                    editButton.click(function () {
                        editResident(resident.residentID);
                    });
                    removeButton.click(function () {
                        removeResident(resident.residentID);
                    });
                    listItem.append(residentInfo);
                    $("#residentList").append(listItem);
                });
            },
            error: function () {
                console.log("Failed to retrieve residents.");
            },
        });
    }
    function removeResident(residentID) {
        $.ajax({
            type: "DELETE",
            url: "/api/residents/" + residentID,
            success: function () {
                displayResidents(); 
            },
            error: function () {
                console.log("Failed to remove resident.");
            },
        });
    }

    function editResident(residentID) {
        $.ajax({
            type: "GET",
            url: "/api/residents/" + residentID,
            success: function (resident) {
                $("#residentID").val(resident.residentID);
                $("#firstName").val(resident.firstName);
                $("#lastName").val(resident.lastName);
                $("#roomNumber").val(resident.roomNumber);
                var formattedAdmissionDate = new Date(resident.admissionDate);
                var dater = formattedAdmissionDate.getFullYear() + "-" +
                    String(formattedAdmissionDate.getMonth() + 1).padStart(2, '0') + "-" +
                    String(formattedAdmissionDate.getDate()).padStart(2, '0');

                $("#admissionDate").val(dater);
                $("#medicalCondition").val(resident.medicalCondition);

                updateButton.off("click").click(function (e) {
                    e.preventDefault();
                    updateResident(resident.residentID);
                });

                cancelButton.off("click").click(function (e) {
                    e.preventDefault();
                    clearForm();
                });

                if (!updateButton.parent().length) {
                    addButton.replaceWith(updateButton);
                    updateButton.after(cancelButton);
                }
            },
            error: function () {
                console.log("Failed to retrieve resident for editing.");
            },
        });
    }

    function updateResident(residentID) {
        var formData = {
            residentID: residentID,
            firstName: $("#firstName").val(),
            lastName: $("#lastName").val(),
            roomNumber: $("#roomNumber").val(),
            admissionDate: $("#admissionDate").val(), 
            medicalCondition: $("#medicalCondition").val()
        };

        $.ajax({
            type: "PUT",
            url: "/api/residents/" + residentID, 
            contentType: "application/json",
            data: JSON.stringify(formData),
            success: function () {
                displayResidents();
                clearForm(); 
            },
            error: function () {
                console.log("Failed to update resident.");
            },
        });
    }

    function clearForm() {
        $("#residentForm")[0].reset();
        updateButton.replaceWith(addButton);
        cancelButton.remove(); 
    }



    displayResidents();

    $("#residentForm").submit(function (event) {
        event.preventDefault();
        var formData = {
            firstName: $("#firstName").val(),
            lastName: $("#lastName").val(),
            roomNumber: $("#roomNumber").val(),
            admissionDate: $("#admissionDate").val(),
            medicalCondition: $("#medicalCondition").val()
        };

        $.ajax({
            type: "POST",
            url: "/api/residents",
            contentType: "application/json",
            data: JSON.stringify(formData),
            success: function () {
                displayResidents();
                $("#firstName").val("");
                $("#lastName").val("");
                $("#roomNumber").val("");
                $("#admissionDate").val("");
                $("#medicalCondition").val("");
            },
            error: function () {
                console.log("Failed to add resident.");
            },
        });
    });
});
