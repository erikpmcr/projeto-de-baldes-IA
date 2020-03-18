using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DepthSearch : MonoBehaviour
{
    public Vector2 startCond;
    public Vector2 endCond;

    public Text b1In;
    public Text b2In;

    public Text b1En;
    public Text b2En;

    public int b1Max = 4;
    public int b2Max = 3;

    public bool b1X = false;
    public bool b2X = false;

    public string s_searchTree;
    public string s_traveledNodes;
    public string s_finalPath;
    public string s_command;
    public string s_mensagem;

    public Text t_searchTree;
    public Text t_traveledNodes;
    public Text t_finalPath;
    public Text t_command;
    public Text t_mensagem;


    public Bucket bucket1;
    public Bucket bucket2;


    [SerializeField]
    public Stack<BucketStateNode> DepthSearchStack = new Stack<BucketStateNode>();
    [SerializeField]
    public List<BucketStateNode> SearchEx = new List<BucketStateNode>();
    [SerializeField]
    public List<BucketStateNode> traveledNodes = new List<BucketStateNode>();
    [SerializeField]
    public List<BucketStateNode> finalPath = new List<BucketStateNode>();

    public bool start = false;
    public bool end = false;
    public bool reset = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!start)
        {
            
            startCond.x = (b1In.text != "") ? Mathf.Clamp(int.Parse(b1In.text),0,b1Max) : 0;
            startCond.y = (b2In.text != "") ? Mathf.Clamp(int.Parse(b2In.text),0,b2Max): 0;
            endCond.x = (b1En.text != "") ? Mathf.Clamp(int.Parse(b1En.text),0,b1Max): 0;
            endCond.y = (b2En.text != "") ? Mathf.Clamp(int.Parse(b2En.text),0,b2Max): 0;

            b1In.text = (b1In.text != "") ? ((int.Parse(b1In.text)!= startCond.x) ? startCond.x.ToString() : b1In.text) : "0";
            b2In.text = (b2In.text != "") ? ((int.Parse(b2In.text) != startCond.y) ? startCond.y.ToString() : b2In.text) : "0";
            b1En.text = (b1En.text != "") ? ((int.Parse(b1En.text) != endCond.x) ? endCond.x.ToString() : b1En.text) : "0";
            b2En.text = (b2En.text != "") ? ((int.Parse(b2En.text) != endCond.y) ? endCond.y.ToString() : b2En.text) : "0";
        }
        else
        {
            if (!end)
            {
                BucketStateNode root = new BucketStateNode();
                root.data = startCond;
                nodeOpenFiller(root);
                nodeChildOpen(root);






                if (root.data == endCond || (root.data.x == endCond.x && b2X) || (root.data.y == endCond.y && b1X))
                {
                    traveledNodes.Add(root);
                    finalPath.Add(root);
                    t_finalPath.text = s_finalPath;
                    t_command.text = s_command;
                    t_traveledNodes.text = s_traveledNodes;
                    t_mensagem.text = s_mensagem + " " + "Pesquisa concluida com sucesso valor inicial e final são iguais";
                }
                else
                {

                    DepthSearchStack.Push(root);

                    trueSearch(DepthSearchStack, endCond);

                }

                end = true;
            }

        }

        if (reset)
        {
            DepthSearchStack.Clear();
            traveledNodes.Clear();
            finalPath.Clear();
            start = true;
            end = false;
            reset = false;
        }

    }

    public void trueSearch(Stack<BucketStateNode> q, Vector2 result)
    {
        if (q.Count != 0)
        {
            BucketStateNode cur;
            if (q.Peek().data == result || (q.Peek().data.x == result.x && b2X) || (q.Peek().data.y == result.y && b1X))
            {
                DepthSearchStack = q;
                traveledNodes.Add(q.Peek());
                BucketStateNode n = q.Peek();

                List<BucketStateNode> tempFinalPath = new List<BucketStateNode>();

                do
                {
                    tempFinalPath.Add(n);
                    if (n.bp != null)
                    {
                        n = n.bp;
                    }
                } while (n.bp != null);

                while (tempFinalPath.Count != 0)
                {
                    finalPath.Add(tempFinalPath[tempFinalPath.Count - 1]);
                    tempFinalPath.RemoveAt(tempFinalPath.Count - 1);
                }

                t_finalPath.text = s_finalPath;
                t_command.text = s_command;

                for (int k=0; k < finalPath.Count; k++)
                {
                    t_finalPath.text += " " + finalPath[k].data.ToString();
                    string tempS;
                    switch (finalPath[k].inCommand)
                    {
                        case Action.empty1:
                            tempS = "esva1";
                            break;
                        case Action.empty2:
                            tempS = "esva2";
                            break;
                        case Action.fill1:
                            tempS = "ench1";
                            break;
                        case Action.fill2:
                            tempS = "ench2";
                            break;
                        case Action.tranf1t2:
                            tempS = "transf1para2";
                            break;
                        case Action.tranf2t1:
                            tempS = "transf2para1";
                            break;
                        default:
                            tempS = "Raiz";
                            break;
                    }
                    t_command.text += " " + tempS;
                }

                t_traveledNodes.text = s_traveledNodes;
                for (int k = 0; k < traveledNodes.Count; k++)
                {
                    t_traveledNodes.text += " " + traveledNodes[k].data.ToString();
                }
                // t_searchTree.text = s_searchTree;

                bucket1.bucketCurrent = Mathf.FloorToInt(endCond.x);
                bucket2.bucketCurrent = Mathf.FloorToInt(endCond.y);
            
                t_mensagem.text = s_mensagem + " " + "Pesquisa concluida com sucesso";

            }
            else
            {
                bool alreadyChecked = false;
                foreach (BucketStateNode n in traveledNodes)
                {
                    if (n.data == q.Peek().data)
                    {
                        alreadyChecked = true;
                    }
                }
                if (!alreadyChecked)
                {

                    cur = q.Pop();
                    traveledNodes.Add(cur);
                    nodeChildOpen(cur);
                    foreach (BucketStateNode n in cur.bc)
                    {
                        q.Push(n);
                    }
                }
                else
                {
                    q.Pop();
                }
                trueSearch(q, result);

            }
        }
        else
        {
            t_mensagem.text = (s_mensagem + " " + "resposta não encontrada");
        }

    }


    public void nodeChildOpen(BucketStateNode n)
    {
        if (n.canEmpty1)
        {
            BucketStateNode nw = new BucketStateNode();
            nw.data = new Vector2(0, n.data.y);

            if (canCreate(nw, n))
            {
                nw.inCommand = Action.empty1;
                nodeOpenFiller(nw);
                n.bc.Add(nw);
                nw.bp = n;
            }
        }
        if (n.canEmpty2)
        {
            BucketStateNode nw = new BucketStateNode();
            nw.data = new Vector2(n.data.x, 0);

            if (canCreate(nw, n))
            {
                nw.inCommand = Action.empty2;
                nodeOpenFiller(nw);
                n.bc.Add(nw);
                nw.bp = n;
            }
        }
        if (n.canFill1)
        {
            BucketStateNode nw = new BucketStateNode();
            nw.data = new Vector2(b1Max, n.data.y);

            if (canCreate(nw, n))
            {
                nw.inCommand = Action.fill1;
                nodeOpenFiller(nw);
                n.bc.Add(nw);
                nw.bp = n;
            }
        }
        if (n.canFill2)
        {
            BucketStateNode nw = new BucketStateNode();
            nw.data = new Vector2(n.data.x, b2Max);

            if (canCreate(nw, n))
            {
                nw.inCommand = Action.fill2;
                nodeOpenFiller(nw);
                n.bc.Add(nw);
                nw.bp = n;
            }
        }
        if (n.canTransfer1t2)
        {
            BucketStateNode nw = new BucketStateNode();
            nw.data = (n.data.x + n.data.y < b2Max) ? new Vector2(0, n.data.x + n.data.y) : new Vector2(n.data.x + n.data.y - b2Max, b2Max);

            if (canCreate(nw, n))
            {
                nw.inCommand = Action.tranf1t2;
                nodeOpenFiller(nw);
                n.bc.Add(nw);
                nw.bp = n;
            }
        }
        if (n.canTransfer2t1)
        {
            BucketStateNode nw = new BucketStateNode();
            nw.data = (n.data.x + n.data.y < b1Max) ? new Vector2(n.data.x + n.data.y, 0) : new Vector2(b1Max, n.data.x + n.data.y - b1Max);

            if (canCreate(nw, n))
            {
                nw.inCommand = Action.tranf2t1;
                nodeOpenFiller(nw);
                n.bc.Add(nw);
                nw.bp = n;
            }
        }
    }

    public bool canCreate(BucketStateNode nw, BucketStateNode n)
    {
        bool canCreate = true;
        if (n.bc.Count > 0)
        {
            foreach (BucketStateNode nn in n.bc)
            {
                if (canCreate && nn.data == nw.data)
                {
                    canCreate = false;
                }
            }
        }
        return canCreate;
    }

    public void nodeOpenFiller(BucketStateNode n)
    {
        if (n.data.x < b1Max)
        {
            n.canFill1 = true;
        }
        if (n.data.y < b2Max)
        {
            n.canFill2 = true;
        }
        if (n.data.x > 0)
        {
            n.canEmpty1 = true;
        }
        if (n.data.y > 0)
        {
            n.canEmpty2 = true;
        }
        if (n.data.y < b2Max && n.data.x > 0)
        {
            n.canTransfer1t2 = true;
        }
        if (n.data.x < b1Max && n.data.y > 0)
        {
            n.canTransfer2t1 = true;
        }

    }

    public void b1xChange()
    {
        b1X = !b1X;
    }

    public void b2xChange()
    {
        b2X = !b2X;
    }

    public void activate()
    {
        start = true;
        if (end)
        {
            reset = true;
        }
    }
}