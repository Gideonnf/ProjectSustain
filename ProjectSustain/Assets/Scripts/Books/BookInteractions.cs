using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class BookInteractions : MonoBehaviour
{
    private static string jsondir = "Books/json";
    private static string imgdir = "Books/Images";
    public TextAsset textJSON;
    public List<string> entries;
    public dataFormat data;
    public int currentPage;

    public GameObject GlossaryObject;
    public GameObject ContentObject;

    public TMP_Text title;
    public Image leftPageImage;
    public TMP_Text leftPageContent;
    public TMP_Text rightPageContent;

    public GameObject buttonNextPage;
    public Transform GlossaryButtonContainer;
    public GameObject GlossaryButton;

    [System.NonSerialized] public int activeButton = 0;
    [System.NonSerialized] public List<GameObject> ListOfButtons = new List<GameObject>();
    [System.NonSerialized] public bool inEntry = false;

    // Start is called before the first frame update
    void Start()
    {
        saveJson();
        loadGlossary();
    }

    void loadGlossary()
    {
        importData();
        foreach (string file in entries)
        {
            title.text = JsonUtility.FromJson<dataFormat>(File.ReadAllText(file)).title;
            GameObject btnGlossary = Instantiate(GlossaryButton);
            btnGlossary.transform.parent = GlossaryButtonContainer;
            GlossaryButton buttonScript = btnGlossary.GetComponent<GlossaryButton>();
            buttonScript.filename = file;
            buttonScript.title = JsonUtility.FromJson<dataFormat>(File.ReadAllText(file)).title;
            buttonScript.interactions = this;
            buttonScript.Init();

            ListOfButtons.Add(btnGlossary);
        }

        activeButton = 0;
        ListOfButtons[0].GetComponent<GlossaryButton>().myText.text = "> " + ListOfButtons[0].GetComponent<GlossaryButton>().title;
    }
    
    public void ScrollBook(bool Up)
    {
        if (Up)
        {
            // cant go above 0
            if (activeButton <= 0)
            {
                return;
            }
            ListOfButtons[activeButton].GetComponent<GlossaryButton>().myText.text = ListOfButtons[activeButton].GetComponent<GlossaryButton>().title;
            activeButton--;
            ListOfButtons[activeButton].GetComponent<GlossaryButton>().myText.text = "> " + ListOfButtons[activeButton].GetComponent<GlossaryButton>().title;
        }
        else
        {
            // cant go below the max number
            if (activeButton >= ListOfButtons.Count - 1)
            {
                return;
            }
            ListOfButtons[activeButton].GetComponent<GlossaryButton>().myText.text = ListOfButtons[activeButton].GetComponent<GlossaryButton>().title;
            activeButton++;
            ListOfButtons[activeButton].GetComponent<GlossaryButton>().myText.text = "> " + ListOfButtons[activeButton].GetComponent<GlossaryButton>().title;

        }
    }

    public void ToggleEntry()
    {
        ListOfButtons[activeButton].GetComponent<GlossaryButton>().onButtonClick();
    }

    public void moveToEntry(string file)
    {
        inEntry = true;
        currentPage = 0;
        GlossaryObject.SetActive(false);
        ContentObject.SetActive(true);
        data = JsonUtility.FromJson<dataFormat>(File.ReadAllText(file));
        displayData(data, 0);
    }

    public void moveToNextPage()
    {
        currentPage++;
        displayData(data, currentPage);
        //unused unless theres newpage
    }


    public void returnToGlossary()
    {
        GlossaryObject.SetActive(true);
        ContentObject.SetActive(false);
        inEntry = false;
    }

    public void importData()
    {
        entries.Clear();
        DirectoryInfo dir = new DirectoryInfo("Assets/Resources/"+jsondir);
        FileInfo[] files = dir.GetFiles();
        foreach (FileInfo file in files)
        {
            if (file.Extension == ".txt")
            {
                entries.Add(file.Directory+ @"\" + file.Name);
            }
        }
    }

    public void displayData(dataFormat data, int page)
    {
        title.text = "";
        leftPageContent.text = "";
        rightPageContent.text = "";

        title.text = data.title;
        leftPageContent.text = data.datapages[page].leftcontent;
        rightPageContent.text = data.datapages[page].rightcontent;


        Debug.Log("Assets/Resources/" + data.datapages[page].imgpath + ".png");
        if (File.Exists("Assets/Resources/" + data.datapages[page].imgpath + ".png"))
        {
            Debug.Log("exists");
            leftPageImage.sprite = Resources.Load<Sprite>(data.datapages[page].imgpath);
            /*Texture2D img = new Texture2D(2, 2);
            img.LoadImage(File.ReadAllBytes(data.datapages[page].imgpath));
            Sprite sprite = Sprite.Create(img, new Rect(0, 0, img.width, img.height), new Vector2(0, 0), 100.0f, 0, SpriteMeshType.Tight);
            leftPageImage.sprite = sprite;*/

        }
        else
        {
            Debug.Log("no img found");
        }

        if (page == data.datapages.Length - 1)
        {
            buttonNextPage.SetActive(false);
        }
        else
        {
            buttonNextPage.SetActive(true);
        }
    }

    public dataFormat loadJson(string file)
    {
        textJSON = Resources.Load<TextAsset>(file);
        return JsonUtility.FromJson<dataFormat>(textJSON.text);
    }

    public void saveJson()
    {
        data = new dataFormat();
        data.title = "Lettuce";

        datapages pages = new datapages();
        pages.leftcontent = @"High Fibre
Rich in antioxidants
Low calories
";
        pages.imgpath = imgdir + "/" + "Lettuce_slice";
        pages.rightcontent = @"A dieter's dream, romaine lettuce has about 8 calories and 1 to 2 grams of carbohydrates per cup .
Although it's low in fiber, it's high in minerals, such as calcium, phosphorous, magnesium, and potassium.
It's naturally low in sodium. Plus, romaine lettuce is packed with vitamin C, vitamin K, and folate
";

        List<datapages> pagelist = new List<datapages>();
        pagelist.Add(pages);
        
        data.datapages = pagelist.ToArray();

        string json;
        json = JsonUtility.ToJson(data);

        StreamWriter sw = new StreamWriter("Assets/Resources/"+jsondir+"/"+data.title+".txt", false);
        sw.WriteLine(json);
        sw.Close();
    }

    [System.Serializable]
    public class dataFormat
    {
        public string title;
        public datapages[] datapages;
    }

    [System.Serializable]
    public class datapages
    {
        public string leftcontent;
        public string imgpath;
        public string rightcontent;
    }





}

