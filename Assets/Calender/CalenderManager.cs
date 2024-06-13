using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections.Generic;
using System.IO;

public class CalenderManager : MonoBehaviour
{
    public GameObject buttonPrefab;  
    public Transform calendarGrid;   
    public TextMeshProUGUI selectedDateText;    
    public TMP_InputField memoInputField;       
    public TMP_Dropdown monthDropdown;         
    public TMP_Dropdown yearDropdown;           
    public TextMeshProUGUI memoDisplayText;     
    public Color memoDateColor = Color.yellow;  
    public Color selectedDateColor = Color.green; 
    private Dictionary<string, string> dateMemos = new Dictionary<string, string>(); 
    private int currentYear;
    private int currentMonth;
    private string memoFilePath;
    private List<Button> dateButtons = new List<Button>();  

    void Start()
    {
        currentYear = DateTime.Now.Year;
        currentMonth = DateTime.Now.Month;

        if (yearDropdown == null || monthDropdown == null || selectedDateText == null || memoInputField == null || memoDisplayText == null || buttonPrefab == null || calendarGrid == null)
        {
            Debug.LogError("One or more references are not assigned in the Inspector.");
            return;
        }

        memoFilePath = Path.Combine(Application.persistentDataPath, "memos.json");
        LoadMemos();

        PopulateYearDropdown();
        PopulateMonthDropdown();
        GenerateCalendar(currentYear, currentMonth);
    }

    void PopulateYearDropdown()
    {
        yearDropdown.ClearOptions();
        List<string> years = new List<string>();
        for (int year = 1990; year <= 2050; year++)
        {
            years.Add(year.ToString());
        }
        yearDropdown.AddOptions(years);
        yearDropdown.value = years.IndexOf(currentYear.ToString());
        yearDropdown.onValueChanged.AddListener(delegate { OnYearChanged(); });
    }

    void PopulateMonthDropdown()
    {
        monthDropdown.ClearOptions();
        List<string> months = new List<string>();
        for (int month = 1; month <= 12; month++)
        {
            months.Add(month.ToString("00"));
        }
        monthDropdown.AddOptions(months);
        monthDropdown.value = currentMonth - 1;
        monthDropdown.onValueChanged.AddListener(delegate { OnMonthChanged(); });
    }

    void OnYearChanged()
    {
        currentYear = int.Parse(yearDropdown.options[yearDropdown.value].text);
        GenerateCalendar(currentYear, currentMonth);
    }

    void OnMonthChanged()
    {
        currentMonth = monthDropdown.value + 1;
        GenerateCalendar(currentYear, currentMonth);
    }

    void GenerateCalendar(int year, int month)
    {
        if (buttonPrefab == null || calendarGrid == null)
        {
            Debug.LogError("Button Prefab or Calendar Grid is not assigned.");
            return;
        }

        
        foreach (Transform child in calendarGrid)
        {
            Destroy(child.gameObject);
        }
        dateButtons.Clear();

        DateTime firstDayOfMonth = new DateTime(year, month, 1);
        int daysInMonth = DateTime.DaysInMonth(year, month);
        int startDayOfWeek = (int)firstDayOfMonth.DayOfWeek;

        
        for (int i = 0; i < startDayOfWeek; i++)
        {
            Instantiate(buttonPrefab, calendarGrid).GetComponentInChildren<TextMeshProUGUI>().text = "";
        }

        
        for (int day = 1; day <= daysInMonth; day++)
        {
            GameObject newButton = Instantiate(buttonPrefab, calendarGrid);
            TextMeshProUGUI buttonText = newButton.GetComponentInChildren<TextMeshProUGUI>();
            if (buttonText == null)
            {
                Debug.LogError("Button Prefab does not have a TextMeshProUGUI component.");
                return;
            }
            buttonText.text = day.ToString();
            int date = day;
            Button buttonComponent = newButton.GetComponent<Button>();
            buttonComponent.onClick.AddListener(() => OnDateSelected(date));
            dateButtons.Add(buttonComponent);

            
            string dateString = $"{currentYear}-{currentMonth:00}-{day:00}";
            if (dateMemos.ContainsKey(dateString))
            {
                buttonComponent.image.color = memoDateColor;
            }
        }
    }

    void OnDateSelected(int date)
    {
        string dateString = $"{currentYear}-{currentMonth:00}-{date:00}";
        selectedDateText.text = "Selected Date: " + dateString;

        
        foreach (Button btn in dateButtons)
        {
            string btnDateString = $"{currentYear}-{currentMonth:00}-{btn.GetComponentInChildren<TextMeshProUGUI>().text:00}";
            if (dateMemos.ContainsKey(btnDateString))
            {
                btn.image.color = memoDateColor;
            }
            else
            {
                btn.image.color = Color.white; 
            }
        }

        
        Button selectedButton = dateButtons.Find(b => b.GetComponentInChildren<TextMeshProUGUI>().text == date.ToString());
        if (selectedButton != null)
        {
            selectedButton.image.color = selectedDateColor;
        }

        if (dateMemos.TryGetValue(dateString, out string memo))
        {
            memoInputField.text = memo;
            memoDisplayText.text = memo;
        }
        else
        {
            memoInputField.text = "";
            memoDisplayText.text = "No memo for this date.";
        }
    }

    public void SaveMemo()
    {
        if (selectedDateText.text == "Selected Date:")
        {
            Debug.LogError("No date selected.");
            return;
        }

        string dateString = selectedDateText.text.Split(' ')[2];
        string memoText = memoInputField.text;

        if (string.IsNullOrEmpty(memoText))
        {
            if (dateMemos.ContainsKey(dateString))
            {
                dateMemos.Remove(dateString);
                memoDisplayText.text = "No memo for this date.";
            }
        }
        else
        {
            dateMemos[dateString] = memoText;
            memoDisplayText.text = memoText;
        }

        SaveMemosToFile();
        UpdateButtonColors(dateString);
    }

    private void UpdateButtonColors(string dateString)
    {
        Button selectedButton = dateButtons.Find(b => b.GetComponentInChildren<TextMeshProUGUI>().text == dateString.Split('-')[2]);
        if (selectedButton != null)
        {
            if (dateMemos.ContainsKey(dateString))
            {
                selectedButton.image.color = memoDateColor;
            }
            else
            {
                selectedButton.image.color = Color.white; 
            }
        }
    }

    private void SaveMemosToFile()
    {
        string json = JsonUtility.ToJson(new SerializableDictionary<string, string>(dateMemos));
        File.WriteAllText(memoFilePath, json);
    }

    private void LoadMemos()
    {
        if (File.Exists(memoFilePath))
        {
            string json = File.ReadAllText(memoFilePath);
            dateMemos = JsonUtility.FromJson<SerializableDictionary<string, string>>(json).ToDictionary();
        }
    }

    [Serializable]
    private class SerializableDictionary<K, V> : ISerializationCallbackReceiver
    {
        [SerializeField]
        private List<K> keys;
        [SerializeField]
        private List<V> values;
        private Dictionary<K, V> target;

        public SerializableDictionary()
        {
            target = new Dictionary<K, V>();
        }

        public SerializableDictionary(Dictionary<K, V> dictionary)
        {
            target = dictionary;
        }

        public Dictionary<K, V> ToDictionary()
        {
            return target;
        }

        public void OnBeforeSerialize()
        {
            keys = new List<K>(target.Keys);
            values = new List<V>(target.Values);
        }

        public void OnAfterDeserialize()
        {
            target = new Dictionary<K, V>();
            for (int i = 0; i < keys.Count; i++)
            {
                target[keys[i]] = values[i];
            }
        }
    }
}

