using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace Incomeplanner
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        private EditText incomePerHourEntry;
        private EditText workHourPerDayEntry;
        private EditText taxRateEntry;
        private EditText savingsRateEntry;
        private Button clearButton;
        private TextView annualWorkHourSummaryLabel;
        private TextView annualIncomeLabel;
        private TextView taxPayableLabel;
        private TextView annualSavingsLabel;
        private TextView spendableIncomeLabel;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);

            incomePerHourEntry = FindViewById<EditText>(Resource.Id.incomePerHourEntry);
            workHourPerDayEntry = FindViewById<EditText>(Resource.Id.workHourPerDayEntry);
            taxRateEntry = FindViewById<EditText>(Resource.Id.taxRateEntry);
            savingsRateEntry = FindViewById<EditText>(Resource.Id.savingsRateEntry);
            clearButton = FindViewById<Button>(Resource.Id.clearButton);
            annualWorkHourSummaryLabel = FindViewById<TextView>(Resource.Id.annualWorkHourSummaryLabel);
            annualIncomeLabel = FindViewById<TextView>(Resource.Id.annualIncomeLabel);
            taxPayableLabel = FindViewById<TextView>(Resource.Id.taxPayableLabel);
            annualSavingsLabel = FindViewById<TextView>(Resource.Id.annualSavingsLabel);
            spendableIncomeLabel = FindViewById<TextView>(Resource.Id.spendableIncomeLabel);

            clearButton.Click += ClearButton_Click;

            incomePerHourEntry.TextChanged += Input_TextChanged;
            workHourPerDayEntry.TextChanged += Input_TextChanged;
            taxRateEntry.TextChanged += Input_TextChanged;
            savingsRateEntry.TextChanged += Input_TextChanged;

            CalculateValues();
        }

        private void ClearButton_Click(object sender, System.EventArgs e)
        {
            incomePerHourEntry.Text = "";
            workHourPerDayEntry.Text = "";
            taxRateEntry.Text = "";
            savingsRateEntry.Text = "";

            CalculateValues();
        }

        private void Input_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            CalculateValues();
        }

        private void CalculateValues()
        {
            double.TryParse(incomePerHourEntry.Text, out var incomePerHour);
            double.TryParse(workHourPerDayEntry.Text, out var workHourPerDay);
            double.TryParse(taxRateEntry.Text, out var taxRate);
            double.TryParse(savingsRateEntry.Text, out var savingsRate);

            double annualWorkHourSummary = workHourPerDay * 5 * 50;
            double annualIncome = incomePerHour * workHourPerDay * 5 * 50;
            double taxPayable = (taxRate / 100) * annualIncome;
            double annualSavings = (savingsRate / 100) * annualIncome;
            double spendableIncome = annualIncome - annualSavings - taxPayable;

            annualWorkHourSummaryLabel.Text = $"Annual Work Hour Summary: {annualWorkHourSummary} HRS";
            annualIncomeLabel.Text = $"Annual Income: {annualIncome} USD";
            taxPayableLabel.Text = $"Tax Payable: {taxPayable} USD";
            annualSavingsLabel.Text = $"Annual Savings: {annualSavings} USD";
            spendableIncomeLabel.Text = $"Spendable Income: {spendableIncome} USD";
        }
    }
}