using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace GestiCoop.UI
{
    public class EtudiantsViewModel : ViewModelBase
    {
        // Données complètes de référence
        private readonly List<Etudiant> _tousLesEtudiants = new();

        // Collection liée au ListView
        public ObservableCollection<Etudiant> Etudiants { get; } = new();

        // Liste pour le ComboBox
        public List<string> Programmes { get; } = new()
        {
            "Tous les programmes",
            "Informatique",
            "Génie logiciel",
            "Cybersécurité"
        };

        private string _programmeFiltre = "Tous les programmes";
        public string ProgrammeFiltre
        {
            get => _programmeFiltre;
            set
            {
                if (Set(ref _programmeFiltre, value))
                {
                    AppliquerFiltre(); // Filtre dès que la sélection change
                }
            }
        }

        private string _rechercheTexte = string.Empty;
        public string RechercheTexte
        {
            get => _rechercheTexte;
            set
            {
                if (Set(ref _rechercheTexte, value))
                {
                    AppliquerFiltre(); // Filtre lors de la frappe
                }
            }
        }

        public ICommand AjouterCommand { get; }

        public EtudiantsViewModel()
        {
            // Données visibles sur la maquette du cours
            _tousLesEtudiants.Add(new Etudiant { Nom = "Tremblay", Prenom = "Sophie", Programme = "Informatique", Courriel = "s.tremblay@etud.lacite.ca" });
            _tousLesEtudiants.Add(new Etudiant { Nom = "Nguyen", Prenom = "Minh", Programme = "Informatique", Courriel = "m.nguyen@etud.lacite.ca" });
            _tousLesEtudiants.Add(new Etudiant { Nom = "Dubois", Prenom = "Élodie", Programme = "Génie logiciel", Courriel = "e.dubois@etud.lacite.ca" });
            _tousLesEtudiants.Add(new Etudiant { Nom = "Okonkwo", Prenom = "Chidi", Programme = "Cybersécurité", Courriel = "c.okonkwo@etud.lacite.ca" });
            _tousLesEtudiants.Add(new Etudiant { Nom = "Lavoie", Prenom = "Xavier", Programme = "Informatique", Courriel = "x.lavoie@etud.lacite.ca" });

            AppliquerFiltre();

            AjouterCommand = new RelayCommand(_ =>
            {
                // Action pour ouvrir la future fenêtre modale
            });
        }

        public void AppliquerFiltre()
        {
            Etudiants.Clear();
            var resultat = _tousLesEtudiants.AsEnumerable();

            // Filtrage par programme sélectionné
            if (!string.IsNullOrWhiteSpace(ProgrammeFiltre) && ProgrammeFiltre != "Tous les programmes")
            {
                resultat = resultat.Where(e => e.Programme == ProgrammeFiltre);
            }

            // Filtrage par texte tapé
            if (!string.IsNullOrWhiteSpace(RechercheTexte))
            {
                resultat = resultat.Where(e =>
                    e.Nom.Contains(RechercheTexte, StringComparison.OrdinalIgnoreCase) ||
                    e.Prenom.Contains(RechercheTexte, StringComparison.OrdinalIgnoreCase));
            }

            foreach (var item in resultat)
            {
                Etudiants.Add(item);
            }
        }
    }
}