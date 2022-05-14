using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;

public class BookInteractions : MonoBehaviour
{
    private static string jsondir = "Books/json";
    public TextAsset textJSON;
    public List<string> entries;
    public dataFormat data;
    public TMP_Text title;
    public TMP_Text leftPageContent;
    public TMP_Text rightPageContent;

    public Transform GlossaryButtonContainer;
    public GameObject GlossaryButton;

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
        }
    }


    public void moveToNextPage()
    {
        //unused unless theres newpage
    }

    public void returnToGlossary()
    {

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
    }

    public dataFormat loadJson(string file)
    {
        textJSON = Resources.Load<TextAsset>(file);
        return JsonUtility.FromJson<dataFormat>(textJSON.text);
    }

    public void saveJson()
    {
        data = new dataFormat();
        data.title = "Carrots";

        datapages pages = new datapages();
        pages.leftcontent = @"- around 5 times of an adult’s 
  recommended intake of Vitamin A(beta carotene)

- High in Vitamin C, about 10 % of
  daily recommended intake

- Rich in copper, calcium, potassium, 
  manganese, and phosphorus";
        pages.rightcontent = @"Fresh carrots can be enjoyed as they are, 
or can be used raw in vegetable as well as fruit salads.

Its slices easily blend with other common
root vegetables like radish, beets, kohlrabi, 
turnips or with greens/ tomato in mixed salads.

Carrot juice is a refreshing drink, 
enjoyed either alone or with fruit/ vegetable juice.

Carrots complement well with vegetables
like green beans, potato, peas in variety
of recipes either stewed, in curry, stir fries, etc.

In South Asia, delicious and sweet dish,
/""gaajar ka halwa,/"" is prepared using grated carrot,
almonds, cashews, pistachio, butter, sugar, and milk.

The root is also used in the
preparation of cakes, tart,
pudding, soups, borscht, etc.

They are also employed in the
preparation of healthy baby foods.
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
        public string rightcontent;
    }





}
