
// const state = {
//     currentView: "home",
//     plants: [],
//     currentPlant: null,
//     careLogs: [],
//     communityTips: [],
// };

// const view = (state) => {
//     switch (state.currentView) {
//         case "home":
//             return fetchAndRenderView("/Home/Index");
//         case "myplants":
//             return fetchAndRenderView("/MyPlant/Index");
//         case "addplant":
//             return fetchAndRenderView("/MyPlant/AddPlant");
//         case "carelog":
//             return fetchAndRenderView("/CareLog/Index");
//         case "communitytips":
//             return fetchAndRenderView("/CommunityTip/Index");
//         case "profile":
//             return fetchAndRenderView("/Profile/Index");
//         case "admin":
//             return fetchAndRenderView("/Admin/Index");
//         case "plantdetails":
//             return fetchAndRenderView(
//                 `/MyPlant/GetPlantDetails/${state.currentPlant}`
//             );
//         default:
//             return fetchAndRenderView("/Home/Index");
//     }
// };

// const update = {
//     Home: (state) => ({ ...state, currentView: "home" }),
//     MyPlants: (state) => ({ ...state, currentView: "myplants" }),
//     AddPlant: (state) => ({ ...state, currentView: "addplant" }),
//     CareLog: (state) => ({ ...state, currentView: "carelog" }),
//     CommunityTips: (state) => ({ ...state, currentView: "communitytips" }),
//     Profile: (state) => ({ ...state, currentView: "profile" }),
//     Admin: (state) => ({ ...state, currentView: "admin" }),

//     ViewPlantDetails: (state, plantId) => ({
//         ...state,
//         currentView: "plantdetails",
//         currentPlant: plantId,
//     }),
//     BackToPlantList: (state) => ({ ...state, currentView: "myplants" }),
//     DeletePlant: async (state, plantId) => {
//         if (confirm("Are you sure you want to delete this plant?")) {
//             await fetch(`/MyPlant/Delete/${plantId}`, { method: "POST" });
//             return { ...state, currentView: "myplants" };
//         }
//         return state;
//     },
//     EditPlant: (state, plantId) => {
//         window.location.href = `/MyPlant/EditPlant/${plantId}`;
//         return state;
//     },
// };

// async function fetchAndRenderView(url) {
//     try {
//         const response = await fetch(url, {
//             headers: {
//                 "X-Requested-With": "XMLHttpRequest",
//             },
//         });

//         if (response.ok) {
//             const html = await response.text();
//             return html;
//         } else {
//             return `<div class="alert alert-danger">Error loading content: ${response.status}</div>`;
//         }
//     } catch (error) {
//         console.error("Error fetching view:", error);
//         return `<div class="alert alert-danger">Error loading content: ${error.message}</div>`;
//     }
// }

// const app = new apprun.App("apprun-app", state, view, update);

// document.addEventListener("submit", async function (event) {
//     const form = event.target;

//     if (form.getAttribute("data-apprun-form") === "true") {
//         event.preventDefault();

//         try {
//             const formData = new FormData(form);
//             const response = await fetch(form.action, {
//                 method: form.method,
//                 body: formData,
//                 headers: {
//                     "X-Requested-With": "XMLHttpRequest",
//                 },
//             });

//             if (response.ok) {
//                 const result = await response.text();

//                 document.getElementById("apprun-app").innerHTML = result;

//                 const redirectTo = form.getAttribute("data-apprun-redirect");
//                 if (redirectTo) {
//                     app.run(redirectTo);
//                 }
//             } else {
//                 console.error("Form submission error:", response.status);
//             }
//         } catch (error) {
//             console.error("Form submission error:", error);
//         }
//     }
// });

// document.addEventListener("click", function (event) {
//     if (event.target.classList.contains("view-details")) {
//         event.preventDefault();
//         const plantId = event.target.getAttribute("data-id");
//         app.run("ViewPlantDetails", plantId);
//     }

//     if (event.target.classList.contains("back-to-list-btn")) {
//         event.preventDefault();
//         app.run("BackToPlantList");
//     }

//     if (event.target.classList.contains("delete-plant")) {
//         event.preventDefault();
//         const plantId = event.target.getAttribute("data-id");
//         app.run("DeletePlant", plantId);
//     }

//     if (event.target.classList.contains("edit-plant-btn")) {
//         event.preventDefault();
//         const plantId = event.target.getAttribute("data-id");
//         app.run("EditPlant", plantId);
//     }
// });

// document.addEventListener("DOMContentLoaded", function () {
//     console.log("AppRun SPA initialized");
// });
