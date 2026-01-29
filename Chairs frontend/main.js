// --- KONFIGURATION ---
// Her definerer vi adressen til din backend (API).
// Lige nu peger den på din Azure Cloud løsning.
// Hvis du vil teste lokalt igen, skal du bare ændre denne streng til "https://localhost:7xxx/api/chairs"
const baseUrl = "http://localhost:5263/api/chairs";

// --- VUE APPLIKATIONEN STARTER HER ---
const app = Vue.createApp({
    
    // 1. DATA (Hukommelsen)
    // data() metoden returnerer de variabler, som HTML-siden kan "se" og bruge.
    // Vue holder hele tiden øje med disse. Hvis du ændrer noget her, opdateres HTML'en automatisk (Reactivity).
    data() {
        return {
            // En tom liste, der skal fyldes med stole fra databasen.
            // HTML'en bruger v-for til at loope igennem denne liste.
            chairs: [],
            
            // Dette objekt er bundet til dine input-felter i HTML'en via 'v-model'.
            // Når brugeren skriver i feltet, opdateres variablen her med det samme (Two-way binding).
            formData: {
                model: "",
                maxWeight: 0,
                hasPillow: false
            }
        }
    },

    // 2. METHODS (Handlinger)
    // Her ligger alle de funktioner, vi kan kalde fra HTML'en (f.eks. ved klik på knapper).
    methods: {
        
        // --- GET: Hent data ---
        // 'async' betyder, at denne funktion arbejder med netværkskald, der tager tid.
        async getAllChairs() {
            try {
                // 'await axios.get(...)' sender en forespørgsel til serveren.
                // Koden "pauser" her, indtil serveren svarer, så vi ikke går videre uden data.
                const response = await axios.get(baseUrl);
                
                // Når data kommer tilbage, gemmer vi dem i vores 'chairs' liste.
                // Fordi det er en Vue-variabel, opdateres tabellen på skærmen øjeblikkeligt.
                this.chairs = response.data;
            } catch (error) {
                // Hvis serveren er nede eller internettet fejler, fanger vi fejlen her.
                alert("Fejl ved hentning af stole. Er API'en startet?");
                console.error(error);
            }
        },

        // --- POST: Opret data ---
        async addChair() {
            try {
                // Vi sender en POST-anmodning til serveren.
                // 'this.formData' indeholder det, brugeren har tastet ind (Model, Vægt, Pude).
                // Serveren læser dette JSON-objekt og gemmer det i databasen.
                await axios.post(baseUrl, this.formData);
                
                // Når stolen er gemt, henter vi hele listen igen.
                // Dette sikrer, at den nye stol vises i tabellen med det samme.
                this.getAllChairs();
                
                // Vi nulstiller formularen, så felterne bliver blanke og klar til næste indtastning.
                this.formData = { model: "", maxWeight: 0, hasPillow: false };
            } catch (error) {
                // Fejlhåndtering: F.eks. hvis valideringen på serveren fejler (navn for kort, vægt for lav).
                alert("Kunne ikke oprette stol. Tjek at modellen er mindst 2 tegn og vægt mindst 50.");
                console.error(error);
            }
        },

        // --- DELETE: Slet data ---
        // Vi skal bruge 'id' for at vide præcis hvilken stol, der skal slettes.
        async deleteChair(id) {
            // En sikkerhedsforanstaltning, så man ikke sletter ved et uheld.
            if (!confirm("Er du sikker på, at du vil slette denne stol?")) return;

            try {
                // Vi bygger URL'en dynamisk. Hvis baseUrl er ".../api/chairs" og id er 5,
                // bliver kaldet til: ".../api/chairs/5"
                await axios.delete(baseUrl + "/" + id);
                
                // Efter sletning henter vi listen igen for at fjerne den slettede stol fra skærmen.
                this.getAllChairs();
            } catch (error) {
                alert("Fejl ved sletning.");
                console.error(error);
            }
        }
    },

    // 3. LIFECYCLE HOOKS (Opstart)
    // 'mounted' er en speciel Vue-funktion. Den kører automatisk én gang, 
    // lige så snart siden er færdigindlæst i browseren.
    mounted() {
        // Vi beder om at hente alle stole med det samme, så listen ikke er tom, når brugeren åbner siden.
        this.getAllChairs();
    }
});

// --- FORBIND TIL HTML ---
// Her fortæller vi Vue, at den skal overtage kontrollen over den <div> i index.html, der har id="app".
app.mount("#app");