//using app.models;

//using system;
//using system.collections.generic;
//using system.net.http;
//using system.text;
//using xamarin.forms;

//namespace app.viewsmodels
//{
//    public class plantviewmodel 
//    {
//        public command saveplantcommand
//        {
//            get
//            {
//                return new command(async () =>
//                {
//                    plant plant = new plant();
//                    plant.id = id;
//                    plant.name = name;

//                    string url = "";
//                    httpclient client = new httpclient();
//                    string jsondata = jsonconvert.serializeobject(plant);
//                    stringcontent content = new stringcontent(jsondata, encoding.utf8, "application/json");
//                    httpresponsemessage response = await client.postasync(url, content);
//                    string result = await response.content.readasstringasync();
//                    response responsedata = jsonconvert.deserializeobject<response>(result);
//                    if (responsedata.status == 1)
//                    {
//                        await navigation.popasync();
//                    }
//                    else
//                    {

//                    }
//                });
//            }
//        }

//        string _id;
//        public string id
//        {
//            get
//            {
//                return _id;
//            }
//            set
//            {
//                if(value != null)
//                {
//                    _id = value;
                    
//                }
//            }
//        }

//        string _name;
//        public string name
//        {
//            get
//            {
//                return _name;
//            }
//            set
//            {
//                if (value != null)
//                {
//                    _name = value;

//                }
//            }
//        }
//    }
//}
