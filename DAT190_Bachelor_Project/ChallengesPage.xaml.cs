using System;
using System.Collections.Generic;
using DAT190_Bachelor_Project.Model;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace DAT190_Bachelor_Project
{
    public partial class ChallengesPage : ContentPage
    {

        ObservableCollection<Challenge> dataSourceList = new ObservableCollection<Challenge>();

        public ChallengesPage()
        {
            InitializeComponent();
            ChallengesListView.ItemsSource = dataSourceList;
            dataSourceList.Add(new Challenge("kiwi_challenge", "Klimanøytrale varer med Kiwi", 10, 2, "78AC94", "Du utfordres til å bytte ut 10 vanlige varer med klimanøytrale varer i Kiwi butikker i løpet av Juni.", "Vi i Kiwi er opptatt av å bevare miljøet samtidig som vi reduserer prisene på frukt og grønt. I samarbeid med Equinor reduserer vi prisene på varer som er klimanøytrale, og utfordrer deg til å redusere ditt karbonavtrykk gjennom å erstatte varer med høyt utslipp, med varer uten utslipp."));
            dataSourceList.Add(new Challenge("equinor_challenge", "Avocado sa du?", 15, 0, "40AC9A", "Du utfordres til å gå 15 dager uten å kjøpe avokado.", "A new study conducted by Carbon Footprint Ltd claims a small pack of two avocados has an emissions footprint of 846.36g CO2, almost twice the size of one kilo of bananas (480g). This is because of the complexities involved in growing, ripening and transporting the popular green fruit. Simon Lee, Founder Director of It’s Fresh!™, said: Avocados have become extremely popular mainly because they’ve been linked to a healthier lifestyle. When you consider everything that has gone into getting this fruit to the consumer - the cultivation, the ripening process and the transportation, then the size of the carbon footprint is not that surprising. It highlights how important it is to prolong the life of the avocado. When you waste food, you waste everything that went into creating it and people don’t always consider this."));
            dataSourceList.Add(new Challenge("platou_challenge", "På hjul med Platou", 10, 4, "B0DBAC", "Du utfordres til å ta sykkelen istedenfor bilen til jobb 10 dager i løpet av Juni.", "Det mangler ikke på gode argumenter for å la bilen stå og heller ta sykkelen når du skal på jobb.En studie publisert i The Lancet Diabetes and Endocrinology journal i vår, viste at voksne som sykler eller går til jobb har en betydelig lavere fettprosent og kroppsmasseindeks(BMI) sammenlignet med voksne som bruker bilen til jobb. I tillegg er det et flott tiltak for å redusere dine CO2 utslipp ved å la bilen stå igjen hjemme. Vi i Platou utfordrer deg til å bytte ut bil med sykkel 10 ganger denne måneden. Gullfotavtrykkene du vinner kan du bruke for å få flotte rabatter i våre butikker."));
            ChallengesListView.ItemTapped += (object sender, ItemTappedEventArgs e) => {
                // don't do anything if we just de-selected the row
                if (e.Item == null) return;
                // do something with e.SelectedItem
                ((ListView)sender).SelectedItem = null; // de-select the row
            };
        }

        public void ChallengeTapped(object sender, EventArgs e)
        {
            var vc = ((ViewCell)sender);
            Challenge ch = (Challenge)vc.BindingContext;
            if (ch == null)
            {
                System.Diagnostics.Debug.WriteLine("No chalenge here");
            }
            Navigation.PushAsync(new ChallengeDetailsPage(ch));
        }


    }
}
