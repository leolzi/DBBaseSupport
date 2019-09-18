using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBBase
{
    public class AttributeSupport
    {
    }

    //老魏的工具生成的累带有的Attribute
    public class DBInfoAtt : Attribute
    {
        public DBInfoAtt(Column col, DataSize si)
        {
            Col = col;
            Datasize = si;
        }

        public Column Col { get; set; }

        public DataSize Datasize { get; set; }
    }
    //字段处理标识
    public enum OperateType
    {
        Default=0,
        Ignore=1,
        Serialize=2,
    }
    //数据库类型标识
    public enum DBType
    {
        Default=0,//所有的都用
        Oracle =1,
        SqlServer=2,
        MySql=3,
        Sqlite=4,
        MogoDB=5,
    }
    //标识数据库表的名称和类型
    public class Table : Attribute
    {
        public Table(string na)
        {
            Name = na;
        }

        public string Name { get; set; }

        public DBType BindDB { get; set; } = DBType.Default;
    }

    //标识列的名称等属性
    public class Column : Attribute
    {
        public Column(OperateType opt)
        {
            OpType = opt;
        }
        public Column(Type t)
        {
            BindType = t;
        }
        public Column(string co, string na)
        {
            Code = co;
            Name = na;
        }
        public Column(string co, string na,Type t)
        {
            Code = co;
            Name = na;
            BindType = t;
        }
        public Column(OperateType opt,Type t)
        {
            OpType = opt;
            BindType = t;
        }

        public Column(bool primary)
        {
            IsPrimary = primary;
        }

        public Column(string co,string na,Type bt,bool prim,OperateType oty)
        {
            Code = co;
            Name = na;
            BindType = bt;
            IsPrimary = prim;
            OpType = oty;
        }

        public string Code { get; set; } = "";
        public string Name { get; set; } = "";
        public Type BindType { get; set; } = null;
        public bool IsPrimary { get; set; } = false;
        public OperateType OpType { get; set; } = OperateType.Default;
    }

    public class DataSize : Attribute
    {
        public DataSize(int si)
        {
            Size = si;
        }
        public DataSize(int si, int si2)
        {
            Size = si;
            SizeTwo = si2;
        }
        public int Size { get; set; }

        public int SizeTwo { get; set; }
    }
}
